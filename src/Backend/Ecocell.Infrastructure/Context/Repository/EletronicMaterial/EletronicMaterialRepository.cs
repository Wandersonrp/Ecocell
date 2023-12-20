using Ecocell.Domain.Repository.EletronicMaterial;

namespace Ecocell.Infrastructure.Context.Repository.EletronicMaterial;

public class EletronicMaterialRepository : IEletronicMaterialWriteOnlyRepository
{
    private readonly EcocellContext _context;

    public EletronicMaterialRepository(EcocellContext context)
    {
        _context = context;
    }

    public async Task<Domain.Entities.EletronicMaterial> Add(Domain.Entities.EletronicMaterial eletronicMaterial)
    {
        await _context.EletronicMaterials.AddAsync(eletronicMaterial);

        return eletronicMaterial;
    }
}