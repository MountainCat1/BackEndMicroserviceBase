using BaseApp.Infrastructure.Abstractions;

namespace BaseApp.Infrastructure.Errors.Database;

public class DuplicateEntryException : DatabaseException
{
    public DuplicateEntryException(string message) : base(message)
    {
    }

    public DuplicateEntryException()
    {
    }
    

    public DuplicateEntryException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}