using PlatformService.Models;

namespace PlatformService.Data.Repositories;

public interface IPlatformRepository
{
    Task<IEnumerable<Platform>> GetAllAsync();
    Task<Platform?> GetAsync(string id);
    Task CreateAsync(Platform platform);
    Task<bool> SaveAsync();
}
