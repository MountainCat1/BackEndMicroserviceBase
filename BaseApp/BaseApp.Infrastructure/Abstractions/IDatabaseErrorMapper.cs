namespace BaseApp.Infrastructure.Abstractions;

public interface IDatabaseErrorMapper
{
    public Task<Exception> MapAsync(DatabaseException exception);
}