using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data.Repositories;

public class PlatformRepository(AppDbContext db) : IPlatformRepository
{
    public async Task CreateAsync(Platform platform)
    {
        if (platform == null) throw new ArgumentNullException(nameof(platform));
        await db.Platform.AddAsync(platform);
    }

    public async Task<IEnumerable<Platform>> GetAllAsync()
    {
        return await db.Platform.ToListAsync();
    }

    public async Task<Platform?> GetAsync(string id)
    {
        return await db.Platform.FirstOrDefaultAsync(x => x.Id == id) ?? null;
    }

    public async Task<bool> SaveAsync()
    {
        return (await db.SaveChangesAsync() > 0);
    }
}
