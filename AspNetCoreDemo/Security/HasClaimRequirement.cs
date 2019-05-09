using Microsoft.AspNetCore.Authorization;

namespace AspNetCoreDemo.Security
{
    public class HasClaimRequirement : IAuthorizationRequirement
    {
        public HasClaimRequirement(string claim, string value)
        {
            Claim = claim;
            Value = value;
        }

        public HasClaimRequirement(string claim) : this (claim, "")
        {
        }

        public string Claim { get; }
        public string Value { get; }
    }
}
