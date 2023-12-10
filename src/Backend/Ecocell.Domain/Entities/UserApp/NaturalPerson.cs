namespace Ecocell.Domain.Entities.UserApp;

public class NaturalPerson : User
{
    public long UserId { get => UserId; set => _ = Id; }
    public bool IsDiscarding { get; set; }
    public DateTime BirthDate { get; set; }                            
}