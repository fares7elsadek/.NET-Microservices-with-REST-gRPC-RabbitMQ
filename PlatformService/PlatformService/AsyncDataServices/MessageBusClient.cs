using System.Text;
using System.Text.Json;
using PlatformService.Dtos;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace PlatformService.AsyncDataServices;

public class MessageBusClient: IMessageBusClient
{
    private readonly IConfiguration _configuration;
    private readonly IConnection _connection;
    private readonly IChannel _channel;


    public MessageBusClient(IConfiguration configuration)
    {
        _configuration = configuration;
        var host = _configuration["RabbitMQHost"];
        var port = _configuration["RabbitMQPort"];
        
        var factory = new ConnectionFactory() {HostName = host, Port = int.Parse(port)};
        try
        {
            _connection = factory.CreateConnectionAsync().Result;
            _channel = _connection.CreateChannelAsync().Result;
            _channel.ExchangeDeclareAsync(exchange: "trigger", type: ExchangeType.Fanout).ConfigureAwait(false);
            _connection.ConnectionShutdownAsync += RabbitMQ_Connection_Shutdown;

            Console.WriteLine("Connected to RabbitMQ Channel");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    public void PublishNewPlatform(PlatformPublishedDto platform)
    {
        var message = JsonSerializer.Serialize(platform);
        if (_connection.IsOpen)
        {
            Console.WriteLine("RABBITMQ Connection is open");
            SendMessage(message);
        }
        else
        {
            Console.WriteLine("RABBITMQ Connection Closed");
        }
    }

    private void SendMessage(string message)
    {
        var body = Encoding.UTF8.GetBytes(message);
        _channel.BasicPublishAsync(exchange: "trigger", routingKey: "", body: body).ConfigureAwait(false);
        Console.WriteLine("Message sent");
    }

    private void Dispose()
    {
        Console.WriteLine("Dispose the connection");
        if (_connection.IsOpen)
        {
            _connection.CloseAsync();
            _channel.Dispose();
        }
    }

    private async Task RabbitMQ_Connection_Shutdown(object sender, ShutdownEventArgs e)
    {
        Console.WriteLine("Connection Shutdown");
    }
}