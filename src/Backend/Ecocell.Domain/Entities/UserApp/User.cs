
namespace Ecocell.Domain.Entities.UserApp;

public class User : BaseEntity
{    
    public Guid ExternalId { get; set; } = Guid.NewGuid();  
    public string Name { get; set; }
    public string Email { get; set; }
    public string Document { get; set; }
    public short Role { get; set; }
    public char Type { get; set; } = 'N';
    public string Password { get; set; }   
    public string Cellphone { get; set; }
    public bool IsActive { get; set; } = true;    
    public DateTime? UpdatedAt { get; set; } 
}