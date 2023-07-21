using DocumentIO.Application.Interfaces.Repositories;
using DocumentIO.Application.Interfaces.Services;
using DocumentIO.Application.Services;
using DocumentIO.Domain.Models;
using DocumentIO.Infrastructure.Data;
using DocumentIO.Infrastructure.Repositories;
using DocumentIO.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShortUrl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
// Add DbContext
services.AddDbContext<DocumentDbContext>(options =>
               options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add repositories
services.AddScoped<IDocumentRepository, DocumentRepository>();
services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IUserDocumentsRepository, UserDocumentsRepository>();
services.AddScoped<ISharedLinkRepository, SharedLinkRepository>();

// Add services
services.AddScoped<IDocumentService, DocumentService>();
services.AddScoped<IUserService, UserService>();
services.AddScoped<ISharedLinkService, SharedLinkService>();
var azureBlobStorageConfig = builder.Configuration.GetSection("AzureBlobStorage").Get<AzureBlobStorageConfig>();

services.AddSingleton<IFileStorageService>(provider =>
    new AzureFileStorageService(azureBlobStorageConfig.ConnectionString, azureBlobStorageConfig.ContainerName));

using (var serviceProvider = services.BuildServiceProvider())
{
    SeedData.Initialize(serviceProvider);
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/favicon.ico", context =>
{
    context.Response.StatusCode = StatusCodes.Status204NoContent;
    return Task.CompletedTask;
});

app.MapGet("/{shortedURL}", context =>
{
    var shortedURL = context.Request.RouteValues["shortedURL"]?.ToString();
    var redirectTo = $"/api/sharedlink/{shortedURL}/download";
    context.Response.Redirect(redirectTo);
    return Task.CompletedTask;
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();

