using Domain.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using WebApi.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddMicrosoftIdentityWebApi(options =>
        {
            builder.Configuration.Bind("AzureAd", options);
            options.TokenValidationParameters.NameClaimType = "name";
        }, options => { builder.Configuration.Bind("AzureAd", options); });

builder.Services.AddAuthorization(config =>
{
    config.AddPolicy("AuthZPolicy", policyBuilder =>
        policyBuilder.Requirements.Add(new ScopeAuthorizationRequirement() { RequiredScopesConfigurationKey = $"AzureAd:Scopes" }));
});

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
