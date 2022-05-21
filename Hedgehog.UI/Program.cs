using Hedgehog.Infrastructure;
using Hedgehog.UI;
using NLog.Web;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureAppConfiguration((_, configuration) =>
{
    configuration.AddJsonFile("appsettings.json", false)
        .AddJsonFile(
            $"appsettings.{Ambience.Name}.json",
            true
        )
        .AddEnvironmentVariables()
        .AddKeyPerFile("/run/secrets", true);
})
.ConfigureWebHostDefaults(webBuilder =>
{
    webBuilder.UseStartup<Startup>();
})
.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.SetMinimumLevel(LogLevel.Trace);
})
.UseNLog();

var app = builder.Build();
app.Run();