using System.ComponentModel.DataAnnotations;
using BaseApp.Application.Errors;
using BaseApp.Infrastructure.Abstractions;

namespace BaseApp.Api.Middlewares;

public class ErrorHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ErrorHandlingMiddleware> _logger;
    private readonly IDatabaseErrorMapper _databaseErrorMapper;

    public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger, IDatabaseErrorMapper databaseErrorMapper)
    {
        _logger = logger;
        _databaseErrorMapper = databaseErrorMapper;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (DatabaseException databaseException)
        {
            var error = await _databaseErrorMapper.MapAsync(databaseException);
            HandleException(context, error);
        }
        catch (Exception ex)
        {
            HandleException(context, ex);
        }
    }

    private void HandleException(HttpContext context, Exception ex)
    {
        switch (ex)
        {
            case NotFoundError error:
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                context.Response.WriteAsync(error.Message);
                break;
            case UnauthorizedError error:
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.WriteAsync(error.Message);
                break;
            case FluentValidation.ValidationException:
            case ValidationException:
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.WriteAsync(ex.Message);
                break;
            default:
                _logger.LogError(ex, ex.Message);
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.WriteAsync("Something went wrong");
                break;
        }
    }
}
