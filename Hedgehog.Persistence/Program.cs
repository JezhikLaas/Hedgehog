using System.Runtime.CompilerServices;
using Hedgehog.Persistence.Actors;
using Hedgehog.Persistence.Infrastructure.Mappings;
using Hedgehog.Persistence.Infrastructure.RabbitMQ;
using Hedgehog.Persistence.Infrastructure.RavenDB;
using NLog.Web;

[assembly:InternalsVisibleTo("Hedgehog.UnitTests")]

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton(MappingConfiguration.CreateMapper());
        services.AddSingleton<IStoreProvider, StoreProvider>();
        services.AddSingleton<IBusProvider, BusProvider>();
        services.AddHostedService<RolesWorker>();
    })
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.SetMinimumLevel(LogLevel.Trace);
    })
    .UseNLog()
    .Build();

await host.RunAsync();