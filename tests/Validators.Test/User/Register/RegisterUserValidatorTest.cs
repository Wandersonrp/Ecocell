using Ecocell.Application.UseCases.UserApp.Register;
using Ecocell.Exceptions;
using FluentAssertions;
using Utilities.Test.Request;

namespace Validators.Test.User.Register;

public class RegisterUserValidatorTest
{
    [Fact]
    public void Validate_Success()
    {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserBuilder.NaturalPersonBuilder();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_Error_Empty_Name()
    {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserBuilder.NaturalPersonBuilder();
        request.Name = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessage.EMPTY_USER_NAME));
    }

    [Fact]
    public void Validate_Error_Empty_Email()
    {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserBuilder.NaturalPersonBuilder();
        request.Email = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessage.EMPTY_EMAIL));
    }

    [Fact]
    public void Validate_Error_Empty_Document()
    {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserBuilder.NaturalPersonBuilder();
        request.Document = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessage.EMPTY_DOCUMENT));
    }

    [Fact]
    public void Validate_Error_Empty_Password()
    {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserBuilder.NaturalPersonBuilder();
        request.Password = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessage.EMPTY_PASSWORD));
    }

    [Fact]
    public void Validate_Error_Empty_Cellphone()
    {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserBuilder.NaturalPersonBuilder();
        request.Cellphone = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessage.EMPTY_CELLPHONE));
    }    

    [Fact]
    public void Validate_Error_Invalid_Email()
    {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserBuilder.NaturalPersonBuilder();
        request.Email = "testedesistema";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessage.INVALID_EMAIL));
    } 

    [Fact]
    public void Validate_Error_Invalid_Cellphone()
    {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserBuilder.NaturalPersonBuilder();
        request.Cellphone = "333 9 33333333";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessage.INVALID_CELLPHONE));
    } 

    [Fact]
    public void Validate_Error_Invalid_User_Type()
    {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserBuilder.NaturalPersonBuilder();
        request.Type = 'H';

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessage.INVALID_USER_TYPE));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    public void Validate_Error_Password_Min_Length(short passwordLength)
    {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserBuilder.NaturalPersonBuilder(passwordLength);
        
        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessage.PASSWORD_MIN_LENGTH));
    }

    [Fact]
    public void Validate_Error_Password_Max_Length()
    {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserBuilder.NaturalPersonBuilder(passwordLength: 21);
        
        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessage.PASSWORD_MAX_LENGTH));
    }  

    [Fact]
    public void Validate_Error_Document_Min_Length_Natural_Person()
    {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserBuilder.NaturalPersonBuilder();
        request.Document = "12345678";
        
        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessage.DOCUMENT_MIN_LENGTH));
    }

    [Fact]
    public void Validate_Error_Document_Max_Length_Natural_Person()
    {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserBuilder.NaturalPersonBuilder();
        request.Document = "123456789101"; //12 characters
        
        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessage.DOCUMENT_MAX_LENGTH));
    }

    [Fact]
    public void Validate_Error_Document_Min_Length_Legal_Person()
    {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserBuilder.LegalPersonBuilder();
        request.Document = "123456789101";
        
        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessage.DOCUMENT_MIN_LENGTH));
    }

    [Fact]
    public void Validate_Error_Document_Max_Length_Legal_Person()
    {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserBuilder.LegalPersonBuilder();
        request.Document = "123456789101125"; //12 characters
        
        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessage.DOCUMENT_MAX_LENGTH));
    }  

    [Fact]
    public void Validate_Error_Empty_Company_Name()
    {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserBuilder.LegalPersonBuilder();
        request.CompanyName = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessage.EMPTY_COMPANY_NAME));
    }

    [Fact]
    public void Validate_Error_Company_Name_Max_Length()
    {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserBuilder.LegalPersonBuilder();
        request.CompanyName = "Lorem Ipsum é simplesmente uma simulação de texto da indústria tipográfica e de impressos, e vem sendo utilizado desde o século XVI, quando um impressor desconhecido pegou uma bandeja de tipos e os embaralhou para fazer um livro de modelos de tipos. Lorem Ipsum sobreviveu não só a cinco séculos, como também ao salto para a editoração eletrônica, permanecendo essencialmente inalterado. Se popularizou na década de 60, quando a Letraset lançou decalques contendo passagens de Lorem Ipsum, e mais recentemente quando passou a ser integrado a softwares de editoração eletrônica como Aldus PageMaker.";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessage.COMPANY_NAME_MAX_LENGTH));
    }

    [Fact]
    public void Validate_Error_Name_Max_Length()
    {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserBuilder.NaturalPersonBuilder();
        request.Name = "Lorem Ipsum é simplesmente uma simulação de texto da indústria tipográfica e de impressos, e vem sendo utilizado desde o século XVI, quando um impressor desconhecido pegou uma bandeja de tipos e os embaralhou para fazer um livro de modelos de tipos. Lorem Ipsum sobreviveu não só a cinco séculos, como também ao salto para a editoração eletrônica, permanecendo essencialmente inalterado. Se popularizou na década de 60, quando a Letraset lançou decalques contendo passagens de Lorem Ipsum, e mais recentemente quando passou a ser integrado a softwares de editoração eletrônica como Aldus PageMaker.";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessage.USER_NAME_MAX_LENGTH));
    }

    [Fact]
    public void Validate_Error_Email_Max_Length()
    {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserBuilder.NaturalPersonBuilder();
        request.Email = "testetestestetestetestetestetestetestetestetestetestetesteteste@teste.com";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessage.EMAIL_MAX_LENGTH));
    }
}