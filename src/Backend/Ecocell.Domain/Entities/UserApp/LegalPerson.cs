namespace Ecocell.Domain.Entities.UserApp;

public class LegalPerson : User
{
    public long UserId { get => UserId; set => _ = Id; }
    public string CompanyName { get; set; }
    public bool IsDiscarding { get; set; }
    public bool IsCollectPoint { get; set; }
    public bool IsCollector { get; set; }    
}