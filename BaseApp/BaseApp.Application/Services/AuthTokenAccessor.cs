using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace TicketTemplateDomain.Application.Services;

public interface IAuthTokenAccessor
{
    string GetToken();
}

public class AuthTokenAccessor : IAuthTokenAccessor
{
    private readonly IHttpContextAccessor _accessor;

    public AuthTokenAccessor(IHttpContextAccessor accessor)
    {
        _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
    }
    
    public string GetToken()
    {
        var authorizationHeaderValue = _accessor.HttpContext?.Request.Headers
            .First(headers => headers.Key == "Authorization").Value.First();

        if (authorizationHeaderValue is null)
            throw new NullReferenceException();

        return authorizationHeaderValue.Substring("Bearer ".Length);
    }
}