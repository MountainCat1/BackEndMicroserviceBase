using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace BaseApp.Application.Authorization;

public abstract class OperationAuthorizationHandler<TResource> : AuthorizationHandler<OperationAuthorizationRequirement, TResource>
{
    protected AuthorizationHandlerContext Context { get; private set; }
    protected OperationAuthorizationRequirement Requirement { get; private set; }
    protected TResource Resource { get; private set; }
    protected ClaimsPrincipal User { get; private set; }
    
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement,
        TResource resource)
    {
        Context = context;
        Requirement = requirement;
        Resource = resource;
        User = Context.User;
        
        if (requirement == Operations.Update)
        {
            HandleResult(await HandleUpdateRequirement());
            return;
        }
        if (requirement == Operations.Create)
        {
            HandleResult(await HandleCreateRequirement());
            return;
        }
        if (requirement == Operations.Delete)
        {
            HandleResult(await HandleDeleteRequirement());
            return;
        }
        if (requirement == Operations.Read)
        {
            HandleResult(await HandleReadRequirement());
            return;
        }
    }

    private void HandleResult(AuthorizationResult result)
    {
        if (result.Succeeded)
        {
            Context.Succeed(Requirement);
        }
        else
        {
            Context.Fail();
        }
    }

    protected abstract Task<AuthorizationResult> HandleCreateRequirement();
    protected abstract Task<AuthorizationResult> HandleReadRequirement();
    protected abstract Task<AuthorizationResult> HandleUpdateRequirement();
    protected abstract Task<AuthorizationResult> HandleDeleteRequirement();


    protected AuthorizationResult Succeed()
    {
        return AuthorizationResult.Success();
    }
    protected AuthorizationResult Fail()
    {
        return AuthorizationResult.Failed();
    }

    // protected class AuthorizationResult
    // {
    //     public bool Success { get; set; } = false;
    // }
}