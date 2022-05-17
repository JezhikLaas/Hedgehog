using AutoMapper;
using Hedgehog.DTOs.Actors;
using Hedgehog.Persistence.Infrastructure.RabbitMQ;
using Hedgehog.Persistence.Infrastructure.RavenDB;

namespace Hedgehog.Persistence.Actors;

public class RolesWorker : BackgroundService
{
    private readonly IBusProvider _busProvider;

    private readonly IStoreProvider _storeProvider;

    private readonly IMapper _mapper;
    public RolesWorker(
        IBusProvider busProvider,
        IStoreProvider storeProvider,
        IMapper mapper)
    {
        _busProvider = busProvider;
        _storeProvider = storeProvider;
        _mapper = mapper;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _busProvider.Connection.Rpc.RespondAsync<CreateRoleRequest, RoleResponse>(
            CreateRole,
            _ => { },
            stoppingToken);
    }

    internal async Task<RoleResponse> CreateRole(CreateRoleRequest request, CancellationToken stoppingToken)
    {
        var role = _mapper.Map<Role>(request);
        var validation = await RoleValidator.Instance.ValidateAsync(role, stoppingToken);

        if (validation.IsValid == false)
        {
            return new RoleResponse
            {
                Error = validation.ToString(";")
            };
        }
        
        using var session = _storeProvider.Store.OpenAsyncSession();
        await session.StoreAsync(role, stoppingToken);
        await session.SaveChangesAsync(stoppingToken);

        return new RoleResponse();
    }
}