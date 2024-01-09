using Ecocell.Communication.Util;
using Newtonsoft.Json;

namespace Ecocell.Communication.Request.UserApp;

public class RequestRegisterUser
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("company_name")]
    public string? CompanyName { get; set; } = null;

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("document")]
    public string Document { get; set; }

    [JsonProperty("birth_date")]
    [Newtonsoft.Json.JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime? BirthDate { get; set; }

    [JsonProperty("role")]
    public short Role { get; set; } 

    [JsonProperty("password")]   
    public string Password { get; set; }

    [JsonProperty("cellphone")]   
    public string Cellphone { get; set; }

    [JsonProperty("type")]
    public char Type { get; set; } = 'N';

    [JsonProperty("is_active")]
    public bool? IsActive { get; set; } = true;

    [JsonProperty("discarding")]
    public bool? IsDiscarding { get; set; } = false;

    [JsonProperty("collect_point")]
    public bool? IsCollectPoint { get; set; } = false;    

    [JsonProperty("collector")]
    public bool? IsCollector { get; set; } = false;    
}