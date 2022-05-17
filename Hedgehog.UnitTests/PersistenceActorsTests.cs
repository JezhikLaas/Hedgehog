using Hedgehog.DTOs.Actors;
using Hedgehog.Persistence.Actors;
using Hedgehog.Persistence.Infrastructure.Mappings;
using Hedgehog.Persistence.Infrastructure.RabbitMQ;
using Hedgehog.UnitTests.Infrastructure;
using NSubstitute;
using Raven.Client.Documents;

namespace Hedgehog.UnitTests;

public class PersistenceActorsTests : RavenTestsBase
{
    [Fact(DisplayName = "If CreateRole gets a valid request Then a role is inserted into store")]
    public async Task If_CreateRole_gets_a_valid_request_Then_a_role_is_inserted_into_store()
    {
        var busProvider = Substitute.For<IBusProvider>();
        var worker = new RolesWorker(busProvider, this, MappingConfiguration.CreateMapper());
        
        var request = new CreateRoleRequest
        {
            Name = "First",
            Description = "First test"
        };
        
        var response = await worker.CreateRole(request, CancellationToken.None);
        Assert.True(response.Ok);

        using var session = Store.OpenAsyncSession();
        var roles = await session.Query<Role>().ToListAsync();

        Assert.Collection(
            roles,
            // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
            // that's the point of unit tests :-)
            x =>
            {
                Assert.Equal("First", x.Name);
                Assert.Equal("First test", x.Description);
            }
        );
    }

    [Fact(DisplayName = "If CreateRole gets an invalid request Then the response indicates an error")]
    public async Task If_CreateRole_gets_an_invalid_request_Then_the_response_indicates_an_error()
    {
        var busProvider = Substitute.For<IBusProvider>();
        var worker = new RolesWorker(busProvider, this, MappingConfiguration.CreateMapper());
        
        var request = new CreateRoleRequest
        {
            Name = "First"
        };
        
        var response = await worker.CreateRole(request, CancellationToken.None);
        Assert.False(response.Ok);
        Assert.NotEmpty(response.Error!);
    }
}