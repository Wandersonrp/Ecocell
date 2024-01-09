using System.Text.RegularExpressions;
using Ecocell.Communication.Request.UserApp;
using Ecocell.Exceptions;
using FluentValidation;

namespace Ecocell.Application.UseCases.UserApp.Register;

public class RegisterUserValidator : AbstractValidator<RequestRegisterUser>
{
    public RegisterUserValidator()
    {        
        RuleFor(c => c.Name).NotEmpty().WithMessage(ResourceErrorMessage.EMPTY_USER_NAME);        
        RuleFor(c => c.Document).NotEmpty().WithMessage(ResourceErrorMessage.EMPTY_DOCUMENT);                
        RuleFor(c => c.Email).NotEmpty().WithMessage(ResourceErrorMessage.EMPTY_EMAIL);
        RuleFor(c => c.Password).NotEmpty().WithMessage(ResourceErrorMessage.EMPTY_PASSWORD);                
        RuleFor(c => c.Cellphone).NotEmpty().WithMessage(ResourceErrorMessage.EMPTY_CELLPHONE);
        RuleFor(c => c.IsDiscarding).NotEmpty().WithMessage(ResourceErrorMessage.EMPTY_DISCARDING_FIELD);
        
        When(c => !string.IsNullOrEmpty(c.Name), () =>
        {
            RuleFor(c => c.Name.Length).LessThanOrEqualTo(100).WithMessage(ResourceErrorMessage.USER_NAME_MAX_LENGTH);
        });

        When(c => !string.IsNullOrWhiteSpace(c.Email), () =>
        { 
            RuleFor(c => c.Email.Length).LessThanOrEqualTo(50).WithMessage(ResourceErrorMessage.EMAIL_MAX_LENGTH);
            RuleFor(c => c.Email).EmailAddress().WithMessage(ResourceErrorMessage.INVALID_EMAIL);
        });

        When(c => c.Type == 'N' && !string.IsNullOrWhiteSpace(c.Document), () =>
        {
            RuleFor(c => c.Document).Custom((document, context) =>
            {
                if(document.Length < 11) 
                {
                    context.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(document), ResourceErrorMessage.DOCUMENT_MIN_LENGTH));
                } else if(document.Length > 11)
                {
                    context.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(document), ResourceErrorMessage.DOCUMENT_MAX_LENGTH));
                }
            });
        });

        When(c => c.Type == 'L' && !string.IsNullOrWhiteSpace(c.Document), () =>
        {
            RuleFor(c => c.Document).Custom((document, context) =>
            {
                if(document.Length < 14) 
                {
                    context.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(document), ResourceErrorMessage.DOCUMENT_MIN_LENGTH));
                } else if(document.Length > 14)
                {
                    context.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(document), ResourceErrorMessage.DOCUMENT_MAX_LENGTH));
                }
            });
        });

        When(c => !string.IsNullOrWhiteSpace(c.Password), () => 
        {
            RuleFor(c => c.Password.Length).GreaterThanOrEqualTo(8).WithMessage(ResourceErrorMessage.PASSWORD_MIN_LENGTH);
            RuleFor(c => c.Password.Length).LessThanOrEqualTo(20).WithMessage(ResourceErrorMessage.PASSWORD_MAX_LENGTH);
        });

        When(c => !string.IsNullOrWhiteSpace(c.Cellphone), () =>
        {
            RuleFor(c => c.Cellphone).Custom((cellphone, context) => 
            {
                string cellphonePattern = "^[0-9]{2} [9]{1} [0-9]{8}$";
                var isMatch = Regex.IsMatch(cellphone, cellphonePattern);

                if(!isMatch)
                {
                    context.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(cellphone), ResourceErrorMessage.INVALID_CELLPHONE));
                }
            });
        });

        When(c => c.Type == 'N' && c.BirthDate != null, () =>
        {
            RuleFor(c => c.BirthDate).Custom((birthDate, context) =>
            {
                var dateUtcNow = DateTime.UtcNow;
                var actualDateLessEightYears = DateTime.UtcNow.AddYears(-8);

                if(birthDate > dateUtcNow)
                {
                    context.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(birthDate), ResourceErrorMessage.INVALID_BIRTH_DATE));
                }

                if(birthDate > actualDateLessEightYears) 
                {
                    context.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(birthDate), ResourceErrorMessage.INVALID_MIN_BIRTH_DATE));
                }
            });
        });

        When(c => !string.IsNullOrEmpty(c.Type.ToString()), () =>
        {
            RuleFor(c => c.Type).Custom((type, context) =>
            {
                if(type != 'N' && type != 'L')
                {
                    context.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(type), ResourceErrorMessage.INVALID_USER_TYPE));
                }
            });
        });

        When(c => c.Type == 'L', () =>
        {
            RuleFor(c => c.CompanyName).NotEmpty().WithMessage(ResourceErrorMessage.EMPTY_COMPANY_NAME);
        });

        When(c => !string.IsNullOrWhiteSpace(c.CompanyName), () =>
        {
            RuleFor(c => c.CompanyName.Length).LessThanOrEqualTo(100).WithMessage(ResourceErrorMessage.COMPANY_NAME_MAX_LENGTH);
        });
    }    
}