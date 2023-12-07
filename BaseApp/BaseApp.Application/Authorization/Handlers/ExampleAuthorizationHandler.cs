using BaseApp.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace BaseApp.Application.Authorization.Handlers
{
    // TODO: Replace placeholder
    public class ExampleAuthorizationHandler : OperationAuthorizationHandler<SomeEntity> // instead of string use your Agregat
    {
        private ILogger<ExampleAuthorizationHandler> _logger;
        private IAuthorizationService _authorizationService;

        public ExampleAuthorizationHandler(
            ILogger<ExampleAuthorizationHandler> logger,
            IAuthorizationService authorizationService)
        {
            _logger = logger;
            _authorizationService = authorizationService;
        }

        protected override async Task<AuthorizationResult> HandleCreateRequirement()
        {
            // var requirement = new IsOrganizerOfRequirement();
            //
            // return await _authorizationService.AuthorizeAsync(User, Resource, requirement);
            throw new NotImplementedException();
        }


        protected override async Task<AuthorizationResult> HandleReadRequirement()
        {
            // var requirement = new IsOrganizerOfRequirement();
            //
            // return await _authorizationService.AuthorizeAsync(User, Resource, requirement);
            throw new NotImplementedException();
        }

        protected override async Task<AuthorizationResult> HandleUpdateRequirement()
        {
            // var requirement = new IsOrganizerOfRequirement();
            //
            // return await _authorizationService.AuthorizeAsync(User, Resource, requirement);
            throw new NotImplementedException();
        }

        protected override async Task<AuthorizationResult> HandleDeleteRequirement()
        {
            // var requirement = new IsOrganizerOfRequirement();
            //
            // return await _authorizationService.AuthorizeAsync(User, Resource, requirement);
            throw new NotImplementedException();
        }
    }
}