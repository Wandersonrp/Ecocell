namespace Ecocell.Communication.Response.Error;

public class ResponseError
{
    public List<string> Messages { get; set; }

    public ResponseError(string message)
    {
        Messages = [message];
    }

    public ResponseError(List<string> messages)
    {
        Messages = messages;
    }
    
}