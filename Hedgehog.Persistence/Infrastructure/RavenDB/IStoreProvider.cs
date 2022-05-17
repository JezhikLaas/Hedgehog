using Raven.Client.Documents;

namespace Hedgehog.Persistence.Infrastructure.RavenDB;

public interface IStoreProvider
{
    IDocumentStore Store { get; }
}