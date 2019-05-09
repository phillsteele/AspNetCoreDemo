using AspNetCoreDemo.Authorization;
using AspNetCoreDemo.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreDemo.Users
{
    public class LsmUser : User
    {
        public const string LsmUsername = "lsm";

        public LsmUser() : base (LsmUsername)
        {
        }

        protected override void AssignClaims()
        {
            // Add a custom list of claims that define what actions the lsm user is able to reference
            //Claims.Add(CustomClaimTypes.FulfilPost, "");
            //Claims.Add(CustomClaimTypes.CanFulfilGet, "");
        }
    }
}
