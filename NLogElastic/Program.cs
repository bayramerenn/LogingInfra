using NLog;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var aspnetEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var environmentSpecificLogFileName = $"nlog.{aspnetEnvironment}.config";

IConfigurationRoot config = new ConfigurationBuilder()
                       .AddJsonFile(path: $"appsettings.{aspnetEnvironment}.json").Build();


NLog.Extensions.Logging.ConfigSettingLayoutRenderer.DefaultConfiguration = config;
var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

logger.Info("start");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.ClearProviders();
builder.Host.UseNLog();

var app = builder.Build();

// Configure the HTTP request pipeline.
 app.UseSwagger();
 app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
