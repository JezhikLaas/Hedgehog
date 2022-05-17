using EasyNetQ;

namespace Hedgehog.Persistence.Infrastructure.RabbitMQ;

public interface IBusProvider
{
    IBus Connection { get; }
}