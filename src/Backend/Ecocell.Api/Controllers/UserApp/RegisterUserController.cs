using Ecocell.Application.UseCases.UserApp.Register;
using Microsoft.AspNetCore.Mvc;

namespace Ecocell.Api.Controllers.UserApp;

[ApiController]
[Route("api/signup")]
public class RegisterUserController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromServices] IRegisterUserUseCase useCase)
    {
        var request = new Communication.Request.UserApp.RequestRegisterUser
        {
            Name = "Teste",
            Email = "teste@teste.com",
            Document = "01958172650",
            Password = "12345678",
            Cellphone = "33 9 9955-0531",
            IsDiscarding = true,
            Role = 0,
            BirthDate = DateTime.ParseExact("2009-05-08 14:40:52,531", "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture),
        };

        var response = await useCase.Execute(request);

        return CreatedAtAction(nameof(Post), response);
    }
}