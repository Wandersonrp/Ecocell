using AutoMapper;
using Ecocell.Application.Services.Criptography;
using Ecocell.Application.Services.Token;
using Ecocell.Communication.Request.UserApp;
using Ecocell.Communication.Response.UserApp;
using Ecocell.Domain.Entities.UserApp;
using Ecocell.Domain.Repository;
using Ecocell.Domain.Repository.UserApp;
using Ecocell.Domain.Repository.WorkUnity;
using Ecocell.Exceptions;
using Ecocell.Exceptions.ExceptionsBase;

namespace Ecocell.Application.UseCases.UserApp.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IMapper _mapper;
    private readonly IWorkUnity _workUnity;
    private readonly PasswordEncryptor _passwordEncryptor;
    private readonly TokenController _tokenController;

    
    public RegisterUserUseCase(IUserWriteOnlyRepository userWriteOnlyRepository, IUserReadOnlyRepository userReadOnlyRepository, IMapper mapper, IWorkUnity workUnity, PasswordEncryptor passwordEncryptor, TokenController tokenController)
    {
        _userWriteOnlyRepository = userWriteOnlyRepository;
        _userReadOnlyRepository = userReadOnlyRepository;
        _mapper = mapper;
        _workUnity = workUnity;
        _passwordEncryptor = passwordEncryptor;
        _tokenController = tokenController;
    }

    public async Task<ResponseRegisterUser> Execute(RequestRegisterUser request)
    {
        await Validate(request);

        User entity;
        
        if(request.Type == 'N') entity =_mapper.Map<NaturalPerson>(request);        
        else  entity = _mapper.Map<LegalPerson>(request);        

        entity.Password = _passwordEncryptor.Encrypt(request.Password);
                 
        await _userWriteOnlyRepository.Add(entity);

        await _workUnity.Commit();

        var token = _tokenController.GenerateToken(entity.Email);

        return new ResponseRegisterUser
        {
            Token = token
        };
    }

    private async Task Validate(RequestRegisterUser request)
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(request);

        var userWithSameEmail = await _userReadOnlyRepository.UserExistsWithTheSameEmail(request.Email);

        if (userWithSameEmail) result.Errors.Add(new FluentValidation.Results.ValidationFailure("email", ResourceErrorMessage.EMAIL_ALREADY_EXISTS));

        var userWithSameDocument = await _userReadOnlyRepository.UserExistsWithTheSameDocument(request.Document);

        if (userWithSameDocument) result.Errors.Add(new FluentValidation.Results.ValidationFailure("document", ResourceErrorMessage.DOCUMENT_ALREADY_EXISTS));

        if(!result.IsValid)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage);
            throw new ValidationErrorsException(errorMessages);
        }
    }
}