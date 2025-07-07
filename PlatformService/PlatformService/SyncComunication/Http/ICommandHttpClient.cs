using PlatformService.Dtos;

namespace PlatformService.SyncComunication.Http;

public interface ICommandHttpClient
{
    public Task SendPlatformToCommand(PlatformReadDto platform);
}