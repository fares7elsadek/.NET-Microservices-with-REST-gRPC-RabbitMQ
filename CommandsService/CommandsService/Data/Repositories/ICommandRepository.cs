using CommandsService.Models;

namespace CommandsService.Data.Repositories;

public interface ICommandRepository
{
    bool SaveChanges();
    
    // Platforms
    IEnumerable<Platforms> GetAllPlatforms();
    void CreatePlatform(Platforms plat);
    bool PlaformExits(string platformId);
    bool ExternalPlatformExists(string externalPlatformId);
    
    // Commands
    IEnumerable<Commands> GetCommandsForPlatform(string platformId);
    Commands GetCommand(string platformId, string commandId);
    void CreateCommand(string platformId, Commands command);
}