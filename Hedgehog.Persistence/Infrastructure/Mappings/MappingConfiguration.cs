using System.Reflection;
using AutoMapper;

namespace Hedgehog.Persistence.Infrastructure.Mappings;

public static class MappingConfiguration
{
    public static IMapper CreateMapper()
    {
        var configuration = new MapperConfiguration(configure =>
        {
            configure.AddMaps(Assembly.GetExecutingAssembly());
        });

        return configuration.CreateMapper();
    }
}