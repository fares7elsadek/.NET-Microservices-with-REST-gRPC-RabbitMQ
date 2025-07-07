using System.Text.Json;
using AutoMapper;
using CommandsService.Data.Repositories;
using CommandsService.Dtos;
using CommandsService.Models;

namespace CommandsService.EventProcessing;

public class EventProcessor(IServiceScopeFactory scopeFactory,IMapper mapper):IEventProcessor
{
    public void ProcessEvent(string message)
    {
        var eventType = DetermineEvent(message);
        switch (eventType)
        {
            case EventType.PlatformPublished:
                AddPlatform(message);
                break;
        }
    }

    private EventType DetermineEvent(string message) =>
        JsonSerializer.Deserialize<GenericEventDto>(message).Event switch
        {
            "Platform_Published" => EventType.PlatformPublished,
            _ => EventType.Undetermined
        };

    private void AddPlatform(string platformPublishedMessage)
    {
        using var scope = scopeFactory.CreateScope();
        var repo = scope.ServiceProvider.GetRequiredService<ICommandRepository>();
        var platformPublishedDto = JsonSerializer.Deserialize<PlatformPublishedDto>(platformPublishedMessage);
        try
        {
            var plat = mapper.Map<Platforms>(platformPublishedDto);
            if(repo.ExternalPlatformExists(plat.ExternalId))
                throw new Exception($"Platform {plat.ExternalId} already exists");
            repo.CreatePlatform(plat);
            repo.SaveChanges();
        }
        catch (Exception ex)
        {
            
        }
        
    }
}

enum EventType
{
    PlatformPublished,
    Undetermined,
}