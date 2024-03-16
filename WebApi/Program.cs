using Domain.Settings;
using WebApi.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .Configure<BlobStorageSettings>(configuration.GetSection("BlobStorage"))
    .Configure<KeyVaultSettings>(configuration.GetSection("KeyVault"));

builder.Services
    .AddSingleton<ILoggerFactory>(serviceProvider => LoggerFactory.Create(builder =>
    {
        builder.AddConsole();
    }))
    .AddSingleton(serviceProvider =>
    {
        var factory = serviceProvider.GetService<ILoggerFactory>();
        return factory?.CreateLogger("Common");
    })
    .AddApiModule()
    .AddRepositoryModule();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    builder.Services
        .AddLocalBlobStorageModule();
}
else
{
    builder.Services
        .AddBlobStorageModule();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
