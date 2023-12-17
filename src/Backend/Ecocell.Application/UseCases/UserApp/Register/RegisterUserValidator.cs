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

        When(c => !string.IsNullOrWhiteSpace(c.Document), () =>
        {
            RuleFor(c => c.Document.Length).GreaterThanOrEqualTo(11).WithMessage(ResourceErrorMessage.DOCUMENT_MIN_LENGTH);
            RuleFor(c => c.Document.Length).LessThanOrEqualTo(14).WithMessage(ResourceErrorMessage.DOCUMENT_MAX_LENGTH);
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
                string cellphonePattern = "[0-9]{2} [1-9]{1} [0-9]{4}-[0-9]{4}";
                var isMatch = Regex.IsMatch(cellphone, cellphonePattern);

                if(!isMatch)
                {
                    context.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(cellphone), ResourceErrorMessage.INVALID_CELLPHONE));
                }
            });
        });

        When(c => c.BirthDate != null, () =>
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
    }
}