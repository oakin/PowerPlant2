using PowerPlant.Api;
using PowerPlant.DataAccess;
using Lamar.Microsoft.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using PowerPlant.Services;
using System.Net.WebSockets;
using System.Net;
using PowerPlant.Domain;
using Serilog;

const string SwaggerTitle = "PowerPlant API";
const string SwaggerRoot = "swagger";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddScoped<IPowerGenerationCalculatorService, PowerGenerationCalculatorService>();
//builder.Services.AddScoped<IProductionPlanGeneratorService, ProductionPlanGeneratorService>();
//builder.Services.AddScoped<Serilog.ILogger>((x) => new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("logs\\log-.txt", rollingInterval: RollingInterval.Day).CreateLogger());
//builder.Services.AddTransient<IBroadcastService, BroadcastService>();
//builder.Services.AddSingleton<IWebSocketManagerService, WebSocketManagerService>();
builder.Services.AddTransient<IComparer<PowerplantProductionDto>, PowerplantCostComparer>();


builder.Services.AddSwaggerGen();


//builder.Services.AddDbContext<PowerPlantDBContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("PowerPlantDBContext")));

builder.Host.UseLamar((context, registry) =>
{
    // register services using Lamar

    // add the controllers
    registry.AddControllers();
});

builder.Services.AddLamar(new LamarMainRegistry(builder.Configuration));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c => { c.RouteTemplate = $"{SwaggerRoot}/{{documentName}}/{SwaggerRoot}.json"; });
    app.UseSwaggerUI(c =>
    {
        c.RoutePrefix = SwaggerRoot;
        c.DocumentTitle = SwaggerTitle;
       
    }
    );

}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseWebSockets();

IWebSocketManagerService webSocketManager = app.Services.GetService<IServiceScopeFactory>().CreateScope().ServiceProvider.GetRequiredService<IWebSocketManagerService>();
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/ws")
    {
        if (context.WebSockets.IsWebSocketRequest)
        {
            WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
            await webSocketManager.AddWebSocket(webSocket).ConfigureAwait(false);
        }
        else
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        }
    }
    else
    {
        await next();
    }

});

app.MapControllers();

app.Run();
