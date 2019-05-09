using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreDemo.Security
{
    public class HasClaimPolicyProvider : IAuthorizationPolicyProvider
    {
        const string POLICY_PREFIX = "HasClaim";

        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (!policyName.StartsWith(POLICY_PREFIX))
                return Task.FromResult<AuthorizationPolicy>(null);

            // Get claim name and value
            var tokens = policyName.Split(':');

            if (tokens.Length != 3)
                return Task.FromResult<AuthorizationPolicy>(null);

            // Assign the claim and tokens
            var claim = tokens[1];
            var value = tokens[2];

            // Assert that the claim is not empty
            if (String.IsNullOrEmpty(claim))
                return Task.FromResult<AuthorizationPolicy>(null);

            // Create our new requirement
            var hasClaimRequirement = new HasClaimRequirement(claim, value);

            // Create the policy and add the requirement
            var policy = new AuthorizationPolicyBuilder();
            policy.Requirements.Add(hasClaimRequirement);

            return Task.FromResult(policy.Build());
        }

        // Handle the default case where no policy has been provided, in this scenario we fall back to the default
        // behaviour which requires an authenticated user.
        public Task<AuthorizationPolicy> GetDefaultPolicyAsync() =>
            Task.FromResult(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build());
    }
}
