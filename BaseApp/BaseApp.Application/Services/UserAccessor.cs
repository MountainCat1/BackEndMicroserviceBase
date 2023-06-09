using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace BaseApp.Application.Services;

public interface IUserAccessor { ClaimsPrincipal User { get; } }

public class UserAccessor : IUserAccessor
{
    private readonly IHttpContextAccessor _accessor;

    public UserAccessor(IHttpContextAccessor accessor)
    {
        _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
    }

    public ClaimsPrincipal User => _accessor?.HttpContext?.User ?? throw new NotImplementedException();
}