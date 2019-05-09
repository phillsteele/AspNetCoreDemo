using Microsoft.AspNetCore.Authorization;
using System;

namespace AspNetCoreDemo.Security
{
    public class HasClaimAuthorizeAttribute : AuthorizeAttribute
    {
        const string POLICY_PREFIX = "HasClaim";

        // Default case where no claim is specified, in this scenario we default to normal AuthorizeAttribute behaviour
        public HasClaimAuthorizeAttribute() { }

        public HasClaimAuthorizeAttribute(string claim, string value)
        {
            if (String.IsNullOrEmpty(claim))
                throw new ArgumentNullException(nameof(claim));

            if (claim.Contains(':'))
                throw new ArgumentException("claim cannot contain the ':' character");

            if (value.Contains(':'))
                throw new ArgumentException("value cannot contain the ':' character");

            SetPolicyName(claim, value);
        }

        public HasClaimAuthorizeAttribute(string claim) : this(claim, "")
        {
        }

        private void SetPolicyName(string claim, string value)
        {
            Policy = $"{POLICY_PREFIX}:{claim}:{value}";
        }

        // Get or set the Age property by manipulating the underlying Policy property
        public string Claim
        {
            get
            {
                if (String.IsNullOrEmpty(Policy))
                    throw new ApplicationException("No claim specified");

                var tokens = Policy.Split(':');
                return tokens[1];
            }
        }

        public string Value
        {
            get
            {
                if (String.IsNullOrEmpty(Policy))
                    throw new ApplicationException("No claim specified");

                var tokens = Policy.Split(':');
                return tokens[2];
            }
        }
    }
}
