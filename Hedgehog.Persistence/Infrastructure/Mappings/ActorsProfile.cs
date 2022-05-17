using AutoMapper;
using Hedgehog.DTOs.Actors;
using Hedgehog.Persistence.Actors;

namespace Hedgehog.Persistence.Infrastructure.Mappings;

public class ActorsProfile : Profile
{
    public ActorsProfile()
    {
        CreateMap<CreateRoleRequest, Role>();
    }
}