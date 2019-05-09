using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreDemo.Security
{
    public class HasClaimHandler : AuthorizationHandler<HasClaimRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasClaimRequirement requirement)
        {
            return Task.CompletedTask;
        }
    }
}
