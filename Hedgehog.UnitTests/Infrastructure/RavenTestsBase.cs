using Hedgehog.Persistence.Infrastructure.RavenDB;
using Raven.Client.Documents;
using Raven.TestDriver;

namespace Hedgehog.UnitTests.Infrastructure;

public abstract class RavenTestsBase : RavenTestDriver, IStoreProvider
{
    protected RavenTestsBase()
    {
        Store = GetDocumentStore();
    }
    
    public IDocumentStore Store { get; }
}