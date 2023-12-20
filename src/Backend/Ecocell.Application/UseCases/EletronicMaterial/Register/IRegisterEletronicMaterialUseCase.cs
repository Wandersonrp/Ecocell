using Ecocell.Communication.Request.EletronicMaterial;
using Ecocell.Communication.Response.EletronicMaterial;

namespace Ecocell.Application.UseCases.EletronicMaterial.Register;

public interface IRegisterEletronicMaterialUseCase
{
    Task<ResponseRegisterEletronicMaterial> Execute(RequestRegisterEletronicMaterial request);
}