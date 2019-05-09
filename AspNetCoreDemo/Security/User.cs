using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace AspNetCoreDemo.Security
{
    public class User : IUser
    {
        public User(string username)
        {
            Username = username;
            Claims = new Dictionary<string, string>();
            AssignDefaultClaims();
            AssignClaims();
        }

        public string Username { get; }

        public Dictionary<string, string> Claims { get; }

        public bool IsAuthenticated { get; private set; } = false;

        public async Task AuthenticateUser(string password)
        {
            // TODO: Replace with a password lookup service
            IsAuthenticated = (password == "123");
        }

        private void AssignDefaultClaims()
        {
            Claims.Add(ClaimTypes.Name, Username);
        }

        /// <summary>
        /// Override in the base class to implement claims for a specific user
        /// </summary>
        protected virtual void AssignClaims()
        {

        }
    }

    public class CustomIdentity : IIdentity
    {
        public string AuthenticationType => throw new System.NotImplementedException();

        public bool IsAuthenticated => throw new System.NotImplementedException();

        public string Name => throw new System.NotImplementedException();
    }
}
