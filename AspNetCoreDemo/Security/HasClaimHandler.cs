using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace AspNetCoreDemo.Security
{
    public class HasClaimHandler : AuthorizationHandler<HasClaimRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasClaimRequirement requirement)
        {
            // Check if the claim exists
            if (context.User.HasClaim(c => c.Type == "Fulfil.Get"))
            {
                // Mark success
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }

    }
}
