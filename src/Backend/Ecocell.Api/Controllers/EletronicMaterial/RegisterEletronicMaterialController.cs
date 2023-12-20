using Ecocell.Application.UseCases.EletronicMaterial.Register;
using Ecocell.Communication.Request.EletronicMaterial;
using Ecocell.Communication.Response.EletronicMaterial;
using Microsoft.AspNetCore.Mvc;

namespace Ecocell.Api.Controllers.EletronicMaterial;

[ApiController]
[Route("/eletronic-material")]
public class RegisterEletronicMaterialController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterEletronicMaterial), StatusCodes.Status201Created)]
    public async Task<IActionResult> RegisterEletronicMaterial([FromBody] RequestRegisterEletronicMaterial request, [FromServices] IRegisterEletronicMaterialUseCase useCase)
    {
        var response = await useCase.Execute(request);

        return CreatedAtAction(nameof(RegisterEletronicMaterial), response);
    }
}