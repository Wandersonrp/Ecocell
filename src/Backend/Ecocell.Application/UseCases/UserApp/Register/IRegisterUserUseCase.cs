using Ecocell.Communication.Request.UserApp;
using Ecocell.Communication.Response.UserApp;

namespace Ecocell.Application.UseCases.UserApp.Register;

public interface IRegisterUserUseCase
{
    Task<ResponseRegisterUser> Execute(RequestRegisterUser request);
}