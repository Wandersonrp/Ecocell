namespace Ecocell.Domain.Entities;

public class EletronicMaterial : BaseEntity
{
    public string Description { get; set; }
    public string Type { get; set; }
    public double Weight { get; set; }
    public int Quantity { get; set; }
    public Guid ExternalId { get; set; } = Guid.NewGuid();
    public DateTime? UpdatedAt { get; set; }
}