namespace AppApi.Infrastructure.Generics;

public class ItemNotFoundException : Exception
{
    public ItemNotFoundException()
    {
    }

    public ItemNotFoundException(string? message) : base(message)
    {
    }
}