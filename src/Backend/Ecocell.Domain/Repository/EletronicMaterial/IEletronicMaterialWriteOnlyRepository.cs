namespace Ecocell.Domain.Repository.EletronicMaterial;

public interface IEletronicMaterialWriteOnlyRepository
{
    Task<Domain.Entities.EletronicMaterial> Add(Domain.Entities.EletronicMaterial eletronicMaterial);
}