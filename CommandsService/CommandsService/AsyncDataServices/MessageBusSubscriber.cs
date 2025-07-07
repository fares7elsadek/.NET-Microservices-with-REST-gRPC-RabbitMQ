using System.Text;
using CommandsService.EventProcessing;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CommandsService.AsyncDataServices;

public class MessageBusSubscriber : BackgroundService
{
    private readonly IConfiguration _configuration;
    private readonly IEventProcessor _eventProcessor;
    private IConnection _connection;
    private IChannel _channel;
    private string _queueName;
    private string _host;
    private string _port;

    public MessageBusSubscriber(IConfiguration configuration, IEventProcessor eventProcessor)
    {
        _configuration = configuration;
        _eventProcessor = eventProcessor;
        _host = _configuration["RabbitMQHost"]!;
        _port = _configuration["RabbitMQPort"]!;
    }

    private async Task RabbitMQSetupAsync()
    {
        var factory = new ConnectionFactory() { HostName = _host, Port = int.Parse(_port) };

        try
        {
            _connection = await factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();

            await _channel.ExchangeDeclareAsync(exchange: "trigger", type: ExchangeType.Fanout);
            var queueDeclareOk = await _channel.QueueDeclareAsync();
            _queueName = queueDeclareOk.QueueName;

            await _channel.QueueBindAsync(queue: _queueName, exchange: "trigger", routingKey: "");

            _connection.ConnectionShutdownAsync += RabbitMQ_Connection_Shutdown;

            Console.WriteLine("✅ Connected to RabbitMQ Channel");
        }
        catch (Exception ex)
        {
            Console.WriteLine("❌ RabbitMQ connection error: " + ex.Message);
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("** Listening to RabbitMQ Queue **");

        await RabbitMQSetupAsync();

        if (_channel == null)
        {
            Console.WriteLine("❌ Channel is null, aborting listener.");
            return;
        }

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body;
            var notificationMessage = Encoding.UTF8.GetString(body.ToArray());
            Console.WriteLine($"Received event: {notificationMessage}");
            _eventProcessor.ProcessEvent(notificationMessage);
        };

        await _channel.BasicConsumeAsync(queue: _queueName, autoAck: true, consumer);
    }

    private Task RabbitMQ_Connection_Shutdown(object sender, ShutdownEventArgs e)
    {
        Console.WriteLine("Connection Shutdown");
        return Task.CompletedTask;
    }

    private void Dispose()
    {
        Console.WriteLine("Disposing the connection...");
        if (_connection?.IsOpen == true)
        {
            _connection.CloseAsync();
            _channel?.Dispose();
        }
    }
}
