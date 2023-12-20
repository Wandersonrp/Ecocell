using AutoMapper;

namespace Ecocell.Application.Services.AutoMapper;

public class AutoMapperConfiguration : Profile
{
    public AutoMapperConfiguration()
    {
        CreateMap<Communication.Request.UserApp.RequestRegisterUser, Domain.Entities.UserApp.LegalPerson>()
            .ForMember(destiny => destiny.Password, config => config.Ignore());

        CreateMap<Communication.Request.UserApp.RequestRegisterUser, Domain.Entities.UserApp.NaturalPerson>()
            .ForMember(destiny => destiny.Password, config => config.Ignore());

        CreateMap<Communication.Request.EletronicMaterial.RequestRegisterEletronicMaterial, Domain.Entities.EletronicMaterial>();             
    }
}