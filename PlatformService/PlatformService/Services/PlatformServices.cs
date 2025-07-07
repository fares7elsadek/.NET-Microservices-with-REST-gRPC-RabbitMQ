using AutoMapper;
using PlatformService.AsyncDataServices;
using PlatformService.Data.Repositories;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncComunication.Http;

namespace PlatformService.Services;

public class PlatformServices(IPlatformRepository repo,
    IMapper mapper,ICommandHttpClient commandHttpClient, IMessageBusClient messageBusClient) : IPlatformService
{
    public async Task CreateAsync(PlatformCreateDto platformCreateDto)
    {
        var platform = mapper.Map<Platform>(platformCreateDto);
        await repo.CreateAsync(platform);
        await repo.SaveAsync();
        var platformRead = mapper.Map<PlatformReadDto>(platform);
        
        // send sync
        try
        {
            await commandHttpClient.SendPlatformToCommand(platformRead);
            Console.WriteLine($"Platform {platformRead.Name} has been created and sent to the CommandService.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"failed to send the request to the CommandService(Sync): {ex.Message}");
        }
        
        // send async

        try
        {
            var message = mapper.Map<PlatformPublishedDto>(platformRead);
            message.Event = "Platform_Published";
            messageBusClient.PublishNewPlatform(message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"failed to send the request to the CommandService(Async): {ex.Message}");
        }
        
    }

    public async Task<IEnumerable<PlatformReadDto>> GetAllAsync()
    {
        var platforms = await repo.GetAllAsync();
        return mapper.Map<IEnumerable<PlatformReadDto>>(platforms);
    }

    public async Task<PlatformReadDto?> GetAsync(string id)
    {
        var platform = await repo.GetAsync(id);
        return platform!=null ? mapper.Map<PlatformReadDto>(platform) : null;
    }
}
