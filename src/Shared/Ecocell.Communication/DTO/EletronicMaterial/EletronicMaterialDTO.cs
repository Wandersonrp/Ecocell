using System.Text.Json.Serialization;

namespace Ecocell.Communication.DTO.EletronicMaterial;

public class EletronicMaterialDTO
{    
    public long Id { get; set; }        
    public string Description { get; set; }
    public string Type { get; set; }
    public double Weight { get; set; }
    public int Quantity { get; set; }

    [JsonPropertyName("external_id")]
    public Guid ExternalId { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; set; }
}