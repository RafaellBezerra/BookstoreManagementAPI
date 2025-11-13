namespace BookstoreManagementApi.Exceptions;

public class NotFoundException : SystemException
{
    public NotFoundException(string message) : base(message) { }
}
