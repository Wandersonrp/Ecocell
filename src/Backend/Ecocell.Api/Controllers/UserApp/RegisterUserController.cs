using Ecocell.Application.UseCases.UserApp.Register;
using Ecocell.Communication.Request.UserApp;
using Ecocell.Communication.Response.UserApp;
using Microsoft.AspNetCore.Mvc;

namespace Ecocell.Api.Controllers.UserApp;

[ApiController]
[Route("/signup")]
public class RegisterUserController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterUser), StatusCodes.Status201Created)]
    public async Task<IActionResult> RegisterUser([FromServices] IRegisterUserUseCase useCase, [FromBody] RequestRegisterUser request)
    {        
        var response = await useCase.Execute(request);

        return CreatedAtAction(nameof(RegisterUser), response);
    }
}