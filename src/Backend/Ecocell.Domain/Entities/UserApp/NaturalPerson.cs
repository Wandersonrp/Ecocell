namespace Ecocell.Domain.Entities.UserApp;

public class NaturalPerson : User
{    
    public bool IsDiscarding { get; set; }
    public DateTime BirthDate { get; set; }                            
}