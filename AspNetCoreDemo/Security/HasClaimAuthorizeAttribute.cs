using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreDemo.Security
{
    public class HasClaimAuthorizeAttribute : AuthorizeAttribute
    {
        private string _claim;

        public HasClaimAuthorizeAttribute(string claim)
        {
            _claim = claim;
        }


        public string Claim
        {
            get
            {
                var x = Policy;
                return "pop";
            }
        }
    }
}
