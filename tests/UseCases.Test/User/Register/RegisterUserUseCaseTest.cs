using Ecocell.Application.UseCases.UserApp.Register;
using Ecocell.Exceptions;
using Ecocell.Exceptions.ExceptionsBase;
using FluentAssertions;
using Utilities.Test.Mapper;
using Utilities.Test.Repositories;
using Utilities.Test.Request;
using Utilities.Test.Services;
using Utilities.Test.WorkUnity;

namespace UseCases.Test.User.Register;

public class RegisterUserUseCaseTest
{
    [Fact]
    public async Task Validate_Natural_Person_Success()
    {
        var request = RequestRegisterUserBuilder.NaturalPersonBuilder();

        var useCase = CreateUseCase();

        var response = await useCase.Execute(request);

        response.Should().NotBeNull();
        response.Token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Validate_Legal_Person_Success()
    {
        var request = RequestRegisterUserBuilder.LegalPersonBuilder();

        var useCase = CreateUseCase();

        var response = await useCase.Execute(request);

        response.Should().NotBeNull();
        response.Token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Validate_Error_Email_Already_Exists()
    {
        var request = RequestRegisterUserBuilder.NaturalPersonBuilder();

        var useCase = CreateUseCase(request.Email);

        Func<Task> action = async () => { await useCase.Execute(request); };

        await action.Should().ThrowAsync<ValidationErrorsException>()
            .Where(exception => exception.ErrorMessages.Count() == 1 && exception.ErrorMessages.Contains(ResourceErrorMessage.EMAIL_ALREADY_EXISTS));
    }

    [Fact]
    public async Task Validate_Error_Empty_Email()
    {
        var request = RequestRegisterUserBuilder.NaturalPersonBuilder();

        var useCase = CreateUseCase(request.Email);
        request.Email = string.Empty;

        Func<Task> action = async () => { await useCase.Execute(request); };

        await action.Should().ThrowAsync<ValidationErrorsException>()
            .Where(exception => exception.ErrorMessages.Count() == 1 && exception.ErrorMessages.Contains(ResourceErrorMessage.EMPTY_EMAIL));
    }

    private RegisterUserUseCase CreateUseCase(string email = "")
    {
        var writeOnlyRepository = UserWriteOnlyRepositoryBuilder.Instance().Builder();
        var readOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().UserExistWithSameEmail(email).Builder();
        var mapper = MapperBuilder.Instance();
        var workUnity = WorkUnityBuilder.Instance().Builder();
        var passwordEncryptor = PasswordEncryportBuilder.Instance();
        var tokenController = TokenControllerBuilder.Instance();

        return new RegisterUserUseCase(writeOnlyRepository, readOnlyRepository,  mapper, workUnity, passwordEncryptor, tokenController);
    }
}