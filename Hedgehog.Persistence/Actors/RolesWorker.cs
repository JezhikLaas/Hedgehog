using AutoMapper;
using Hedgehog.DTOs.Actors;
using Hedgehog.Persistence.Infrastructure.RabbitMQ;
using Hedgehog.Persistence.Infrastructure.RavenDB;
using NLog;
using ILogger = NLog.ILogger;

namespace Hedgehog.Persistence.Actors;

public class RolesWorker : BackgroundService
{
    private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
    
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
        
        Logger.Info("Worker {Name} initialized", nameof(RolesWorker));
    }

    internal async Task<RoleResponse> CreateRole(CreateRoleRequest request, CancellationToken stoppingToken)
    {
        Logger.Debug("Enter {Method}", nameof(CreateRole));
        var role = _mapper.Map<Role>(request);
        var validation = await RoleValidator.Instance.ValidateAsync(role, stoppingToken);

        if (validation.IsValid == false)
        {
            Logger.Error(
                "Validation in {Method} failed: {Errors}",
                nameof(CreateRole),
                validation.ToString()
            );
            
            return new RoleResponse
            {
                Error = validation.ToString(";")
            };
        }
        
        using var session = _storeProvider.Store.OpenAsyncSession();
        await session.StoreAsync(role, stoppingToken);
        await session.SaveChangesAsync(stoppingToken);

        Logger.Debug("{Method} succeeded", nameof(CreateRole));
        return new RoleResponse();
    }
}