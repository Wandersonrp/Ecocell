namespace Ecocell.Exceptions.ExceptionsBase;

public class ValidationErrorsException : EcocellException
{
    public IEnumerable<string> ErrorMessages { get; set; }

    public ValidationErrorsException(IEnumerable<string> errorMessages)
    {
        ErrorMessages = errorMessages;
    }
}