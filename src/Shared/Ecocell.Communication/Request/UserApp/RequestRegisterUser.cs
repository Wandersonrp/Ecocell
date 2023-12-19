using System.Text.Json.Serialization;

namespace Ecocell.Communication.Request.UserApp;

public class RequestRegisterUser
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("company_name")]
    public string? CompanyName { get;  set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("document")]
    public string Document { get; set; }  

    [JsonPropertyName("birth_date")]
    public DateTime? BirthDate { get; set; }  

    [JsonPropertyName("role")]
    public short Role { get; set; } 

    [JsonPropertyName("password")]   
    public string Password { get; set; }

    [JsonPropertyName("phone")]   
    public string Cellphone { get; set; }

    [JsonPropertyName("type")]
    public char Type { get; set; } = 'N';

    [JsonPropertyName("is_active")]
    public bool? IsActive { get; set; } = true;    
        
    [JsonPropertyName("discarding")]   
    public bool IsDiscarding { get; set; }

    [JsonPropertyName("collect_point")]
    public bool? IsCollectPoint { get; set; } = false;    

    [JsonPropertyName("collector")]
    public bool? IsCollector { get; set; } = false;    
}