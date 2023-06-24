namespace BaseApp.Application.Errors;

public class NotFoundError : ApplicationError
{
    public override int StatusCode => 404;

    public NotFoundError(string? message) : base(message)
    {
        
    }

    public NotFoundError(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}