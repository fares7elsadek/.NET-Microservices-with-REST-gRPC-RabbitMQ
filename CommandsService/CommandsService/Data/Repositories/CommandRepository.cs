using CommandsService.Models;

namespace CommandsService.Data.Repositories;

public class CommandRepository(AppDbContext db):ICommandRepository
{
    public bool SaveChanges()
    {
        return (db.SaveChanges() >= 0);
    }

    public IEnumerable<Platforms> GetAllPlatforms()
    {
        return db.Platforms.ToList();
    }

    public void CreatePlatform(Platforms plat)
    {
        if(plat == null)
            throw new ArgumentNullException(nameof(plat));
        db.Platforms.Add(plat);
    }

    public bool PlaformExits(string platformId)
    {
        return db.Platforms.Any(x => x.Id == platformId);
    }

    public bool ExternalPlatformExists(string externalPlatformId)
    {
        return db.Platforms.Any(x => x.ExternalId == externalPlatformId);
    }

    public IEnumerable<Commands> GetCommandsForPlatform(string platformId)
    {
        return db.Commands.Where(x => x.PlatformId == platformId);
    }

    public Commands GetCommand(string platformId, string commandId)
    {
        return db.Commands.
            Where(x => x.Id == commandId && x.PlatformId == platformId)
            .FirstOrDefault();
    }

    public void CreateCommand(string platformId, Commands command)
    {
        if(command == null)
            throw new ArgumentNullException(nameof(command));
        command.PlatformId = platformId;
        db.Commands.Add(command);
    }
}