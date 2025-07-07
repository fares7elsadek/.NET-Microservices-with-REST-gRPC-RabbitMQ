using Microsoft.EntityFrameworkCore;
using PlatformService.AsyncDataServices;
using PlatformService.Data;
using PlatformService.Data.Repositories;
using PlatformService.Data.Seeders;
using PlatformService.Services;
using PlatformService.SyncComunication.Http;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("conn"));
});
builder.Services.AddScoped<IPlatformRepository,PlatformRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IPlatformService,PlatformServices>();
builder.Services.AddHttpClient<ICommandHttpClient,CommandHttpClient>();
builder.Services.AddScoped<IMessageBusClient, MessageBusClient>();

var app = builder.Build();

await PlatformSeeder.Seed(app);
if (app.Environment.IsDevelopment() || true)
{
    app.MapOpenApi();
    app.UseSwaggerUI(opt =>
    {
        opt.SwaggerEndpoint("/openapi/v1.json", "OpenAPI V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
