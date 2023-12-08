namespace BaseApp.Application.Errors;

public class ForbiddenError : ApplicationError
{
    public override int StatusCode => 403;

    public ForbiddenError(string? message) : base(message)
    {
    }

    public ForbiddenError(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}