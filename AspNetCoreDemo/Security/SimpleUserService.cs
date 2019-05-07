using System.Threading.Tasks;

namespace AspNetCoreDemo.Security
{
    public class SimpleUserService : IUserService
    {
        public async Task<bool> Authenticate(string userName, string password)
        {
            // TODO: Replace with proper user and password management
            return (userName == "slcs" && password == "123");
        }
    }
}
