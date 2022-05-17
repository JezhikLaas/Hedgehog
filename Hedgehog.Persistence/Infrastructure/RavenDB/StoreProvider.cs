using Raven.Client.Documents;
using Raven.Embedded;

namespace Hedgehog.Persistence.Infrastructure.RavenDB;

public class StoreProvider : IStoreProvider
{
    public StoreProvider()
    {
        EmbeddedServer.Instance.StartServer(new ServerOptions
        {
            FrameworkVersion = "6.x",
            AcceptEula = true
        });
    }

    private IDocumentStore? _documentStore;

    public IDocumentStore Store =>
        _documentStore ??= EmbeddedServer.Instance.GetDocumentStore("Hedgehog");
}