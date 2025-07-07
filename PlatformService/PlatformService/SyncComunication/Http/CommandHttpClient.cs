using System.Text;
using System.Text.Json;
using PlatformService.Dtos;

namespace PlatformService.SyncComunication.Http;

public class CommandHttpClient(IConfiguration configuration,
    HttpClient httpClient):ICommandHttpClient
{
    public async Task SendPlatformToCommand(PlatformReadDto platform)
    {
        var content = new StringContent(JsonSerializer.Serialize(platform),
            Encoding.UTF8,"application/json");
        
        var response = await httpClient.PostAsync(configuration["CommandService"], content);
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Command successful");
        }
        else
        {
            Console.WriteLine("Command failed");
        }
    }
}