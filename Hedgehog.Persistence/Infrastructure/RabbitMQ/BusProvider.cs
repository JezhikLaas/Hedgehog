using EasyNetQ;

namespace Hedgehog.Persistence.Infrastructure.RabbitMQ;

public class BusProvider : IDisposable, IBusProvider
{
    private const string ConnectionDescriptor = @"host=rabbit;
username=guest;
password=guest;
publisherConfirms=true;
persistentMessages=true";
    
    private readonly IBus _connection;

    public BusProvider()
    {
        _connection = RabbitHutch.CreateBus(ConnectionDescriptor);
    }

    public void Dispose()
    {
        _connection.Dispose();
    }

    public IBus Connection =>
        _connection;
}