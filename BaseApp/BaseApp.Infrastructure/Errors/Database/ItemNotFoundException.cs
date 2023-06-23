using BaseApp.Infrastructure.Abstractions;

namespace BaseApp.Infrastructure.Errors.Database;

public class ItemNotFoundException : DatabaseException
{
    public ItemNotFoundException()
    {
    }

    public ItemNotFoundException(string? message) : base(message)
    {
    }
}