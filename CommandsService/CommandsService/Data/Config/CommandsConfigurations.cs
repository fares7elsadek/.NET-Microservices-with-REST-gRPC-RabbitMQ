using CommandsService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommandsService.Data.Config;

public class CommandsConfigurations:IEntityTypeConfiguration<Commands>
{
    public void Configure(EntityTypeBuilder<Commands> builder)
    {
        builder.HasKey(x => x.Id);
        
        
    }
}