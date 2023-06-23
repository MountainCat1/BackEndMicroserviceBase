namespace BaseApp.Application.Errors;

public class NotFoundError : Exception
{
    public NotFoundError()
    {
    }

    public NotFoundError(string? message) : base(message)
    {
    }

    public NotFoundError(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}