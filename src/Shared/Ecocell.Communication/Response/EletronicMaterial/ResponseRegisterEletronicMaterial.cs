using System.Text.Json.Serialization;

namespace Ecocell.Communication.Response.EletronicMaterial;

public class ResponseRegisterEletronicMaterial
{
    [JsonPropertyName("eletronic_material")]
    public DTO.EletronicMaterial.EletronicMaterialDTO EletronicMaterialDTO { get; set; }        
}