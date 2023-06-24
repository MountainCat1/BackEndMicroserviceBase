namespace BaseApp.Application.Errors;

public class UnauthorizedError : ApplicationError
{
    public override int StatusCode => 401;

    public UnauthorizedError(string? message) : base(message)
    {
    }

    public UnauthorizedError(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}