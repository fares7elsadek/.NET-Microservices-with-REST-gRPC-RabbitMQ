using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data.Seeders;

public class PlatformSeeder
{
    public static async Task Seed(IApplicationBuilder app)
    {
        using var service = app.ApplicationServices.CreateScope();
        var db = service.ServiceProvider.GetService<AppDbContext>();
        if (db != null && db.Database.GetPendingMigrations().Any())
        {
            await db.Database.MigrateAsync();
        }
        if ( db!=null && !db.Platform.Any())
        {
            await db.AddRangeAsync(GetPlatforms);
            await db.SaveChangesAsync();
            Console.WriteLine("Platform seeded successfully ... ");
        }
    }

    public static IEnumerable<Platform> GetPlatforms =>
        [
            new Platform {Id=Guid.NewGuid().ToString(),Name = "AspNet Core" , Publisher = "Microsoft" , Cost="Free" },
            new Platform {Id=Guid.NewGuid().ToString(),Name = "Postgresql" , Publisher = "Postgresql.org" , Cost="Free"},
            new Platform {Id=Guid.NewGuid().ToString(),Name = "MongoDB" , Publisher = "MongoDB" , Cost="Free"}
        ];
}
