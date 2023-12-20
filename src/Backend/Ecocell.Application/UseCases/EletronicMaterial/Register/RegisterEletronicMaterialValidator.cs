using Ecocell.Communication.Request.EletronicMaterial;
using Ecocell.Exceptions;
using FluentValidation;

namespace Ecocell.Application.UseCases.EletronicMaterial.Register;

public class RegisterEletronicMaterialValidator :  AbstractValidator<RequestRegisterEletronicMaterial>
{
    public RegisterEletronicMaterialValidator()
    {
        RuleFor(c => c.Description).NotEmpty().WithMessage(ResourceErrorMessage.EMPTY_MATERIAL_DESCRIPTION);
        
        When(c => !string.IsNullOrEmpty(c.Description), () =>
        {
            RuleFor(c => c.Description.Length).LessThanOrEqualTo(50).WithMessage(ResourceErrorMessage.DESCRIPTION_MAX_LENGTH);
        });

        RuleFor(c => c.Type).NotEmpty().WithMessage(ResourceErrorMessage.EMPTY_MATERIAL_TYPE);

        When(c => !string.IsNullOrEmpty(c.Type), () =>
        {
            RuleFor(c => c.Type.Length).LessThanOrEqualTo(1).WithMessage(ResourceErrorMessage.DESCRIPTION_MAX_LENGTH);
        });

        When(c => c.Weight != null, () =>
        {
            RuleFor(c => c.Weight).Custom((weight, context) =>
            {
                if(weight == 0) 
                {
                    context.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(weight), ResourceErrorMessage.MATERIAL_MIN_WEIGHT));
                }
            });
            
        });

        When(c => c.Quantity != null, () =>
        {
            RuleFor(c => c.Quantity).Custom((quantity, context) =>
            {
                if(quantity == 0) 
                {
                    context.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(quantity), ResourceErrorMessage.MATERIAL_MIN_QUANTITY));
                }
            });
            
        });
    }
}