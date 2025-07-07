using CommandsService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommandsService.Data.Config;

public class PlatformsConfigurations:IEntityTypeConfiguration<Platforms>
{
    public void Configure(EntityTypeBuilder<Platforms> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasMany(x => x.Commands)
            .WithOne(x => x.Platform)
            .HasForeignKey(x => x.PlatformId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}