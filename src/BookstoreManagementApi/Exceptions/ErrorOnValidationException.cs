namespace BookstoreManagementApi.Exceptions;

public class ErrorOnValidationException : SystemException
{
    public List<string> _errorMessages;
    public ErrorOnValidationException(List<string> errors)
    {
        _errorMessages = errors;
    }
}
