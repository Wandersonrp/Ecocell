using System.Text.Json.Serialization;

namespace Ecocell.Communication.Request.EletronicMaterial;

public class RequestRegisterEletronicMaterial
{
    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }
    
    [JsonPropertyName("weight")]
    public double? Weight { get; set; }
    
    [JsonPropertyName("quantity")]
    public double? Quantity { get; set; }        
}