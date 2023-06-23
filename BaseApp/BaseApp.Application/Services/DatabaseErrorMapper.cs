using BaseApp.Application.Errors;
using BaseApp.Infrastructure.Abstractions;
using BaseApp.Infrastructure.Errors.Database;

namespace BaseApp.Application.Services;


public class DatabaseErrorMapper : IDatabaseErrorMapper
{
    public async Task<Exception> MapAsync(DatabaseException exception)
    {
        if (exception is ItemNotFoundException)
            return new NotFoundError(null, exception);


        return exception;
    }
}