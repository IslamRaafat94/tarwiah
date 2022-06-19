using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using NLog.Web;
using nwc.Tarwya.Portal;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();

startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment);

app.Run();