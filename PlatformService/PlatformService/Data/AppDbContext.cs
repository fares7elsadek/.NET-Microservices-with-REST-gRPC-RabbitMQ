using Microsoft.EntityFrameworkCore;
using PlatformService.Data.Config;
using PlatformService.Models;

namespace PlatformService.Data;

public class AppDbContext(DbContextOptions<AppDbContext> opt):DbContext(opt)
{
    public DbSet<Platform> Platform { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PlatformConfigurations).Assembly);
    }
}
