using AutoMapper;
using Ecocell.Application.Services.AutoMapper;

namespace Utilities.Test.Mapper;

public class MapperBuilder
{
    public static IMapper Instance()
    {
        var configuration = new MapperConfiguration(config =>
        {
            config.AddProfile<AutoMapperConfiguration>();
        });

        return configuration.CreateMapper();
    }
}