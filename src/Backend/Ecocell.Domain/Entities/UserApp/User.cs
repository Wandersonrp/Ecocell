using Ecocell.Domain.Enums;

namespace Ecocell.Domain.Entities.UserApp;

public abstract class User : BaseEntity
{    
    public Guid ExternalId { get; set; }    
    public string Name { get; set; }
    public string Email { get; set; }
    public string Document { get; set; }
    public Role Role { get; set; }            
    public string Password { get; set; }   
    public string Cellphone { get; set; }
    public bool IsActive { get; set; }    
    public DateTime UpdatedAt { get; set; }    
}