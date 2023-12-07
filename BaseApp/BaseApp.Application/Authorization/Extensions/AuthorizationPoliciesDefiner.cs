using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace BaseApp.Application.Authorization.Extensions
{
    public static class AuthorizationPoliciesDefiner
    {
        public static IServiceCollection DefineAuthorizationPolicies(this IServiceCollection services)
        {
            // TODO: Replace placeholder
            // Example:
            // services.AddSingleton<IAuthorizationHandler, IsOrganizerOfHandler>();

            services.AddAuthorization(DefineAuthorizationPolicies);

            return services;
        }


        private static void AddPolicyWithRequirements(
            this AuthorizationOptions policyBuilder,
            string policyName,
            IAuthorizationRequirement[] requirements)
        {
            policyBuilder.AddPolicy(policyName, builder => { builder.AddRequirements(requirements); });
        }

        private static void DefineAuthorizationPolicies(AuthorizationOptions options)
        {
            // TODO: Replace placeholder
            // Example:
            // options.AddPolicyWithRequirements(Policies.ReadConvention, new IAuthorizationRequirement[]
            // {
            //     new IsOrganizerOfRequirement()
            // });
        }
    }
}