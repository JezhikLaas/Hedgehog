using System.Runtime.CompilerServices;
using Hedgehog.Persistence.Actors;
using Hedgehog.Persistence.Infrastructure.Mappings;
using Hedgehog.Persistence.Infrastructure.RabbitMQ;
using Hedgehog.Persistence.Infrastructure.RavenDB;

[assembly:InternalsVisibleTo("Hedgehog.UnitTests")]

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton(MappingConfiguration.CreateMapper());
        services.AddSingleton<IStoreProvider, StoreProvider>();
        services.AddSingleton<IBusProvider, BusProvider>();
        services.AddHostedService<RolesWorker>();
    })
    .Build();

await host.RunAsync();