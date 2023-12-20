using AutoMapper;
using Ecocell.Communication.DTO.EletronicMaterial;
using Ecocell.Communication.Request.EletronicMaterial;
using Ecocell.Communication.Response.EletronicMaterial;
using Ecocell.Domain.Repository.EletronicMaterial;
using Ecocell.Domain.Repository.WorkUnity;
using Ecocell.Exceptions.ExceptionsBase;

namespace Ecocell.Application.UseCases.EletronicMaterial.Register;

public class RegisterEletronicMaterialUseCase : IRegisterEletronicMaterialUseCase
{
    private readonly IEletronicMaterialWriteOnlyRepository _writeOnlyRepository;
    private readonly IWorkUnity _workUnity;
    private readonly IMapper _mapper;

    public RegisterEletronicMaterialUseCase(IEletronicMaterialWriteOnlyRepository writeOnlyRepository, IWorkUnity workUnity, IMapper mapper)
    {
        _writeOnlyRepository = writeOnlyRepository;
        _workUnity = workUnity;
        _mapper = mapper;
    }

    public async Task<ResponseRegisterEletronicMaterial> Execute(RequestRegisterEletronicMaterial request)
    {
        Validate(request);

        var eletronicMaterial = _mapper.Map<Domain.Entities.EletronicMaterial>(request);

        await _writeOnlyRepository.Add(eletronicMaterial);

        await _workUnity.Commit();        

        return new ResponseRegisterEletronicMaterial
        {
            EletronicMaterialDTO = new EletronicMaterialDTO             
            {
                Id = eletronicMaterial.Id,
                Description = eletronicMaterial.Description,
                ExternalId = eletronicMaterial.ExternalId,
                Quantity = eletronicMaterial.Quantity,
                Type = eletronicMaterial.Type,
                Weight = eletronicMaterial.Weight,
                CreatedAt = eletronicMaterial.CreatedAt,
                UpdatedAt = eletronicMaterial.UpdatedAt
            }
        };
    }

    private void Validate(RequestRegisterEletronicMaterial request)
    {
        var validator = new RegisterEletronicMaterialValidator();
        var result = validator.Validate(request);

        if(!result.IsValid)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage);
            throw new ValidationErrorsException(errorMessages);
        }
    }
}