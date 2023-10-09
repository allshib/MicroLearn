using Micro.PlaformService.AsyncDataServices;
using Micro.PlaformService.Data;
using Micro.PlaformService.SyncDataServicesHttp;
using Microsoft.EntityFrameworkCore;
using System.Windows.Input;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();

builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();

EnableDataBase(builder);

builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI();
}


//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

PrepDb.PrepPopulation(app, app.Environment.IsProduction());

app.Run();

static void EnableDataBase(WebApplicationBuilder builder)
{
    if (builder.Environment.IsDevelopment())
    {
        Console.WriteLine("Подключение базы данных в памяти");
        builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
    }
    else
    {
        Console.WriteLine("Подключение базы данных SQL Server");
        builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("PlatformsConn")));
    }
}