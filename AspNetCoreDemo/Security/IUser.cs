using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;

namespace AspNetCoreDemo.Security
{
    public interface IUser
    {
        string Username { get; }
        Dictionary<string, string> Claims { get; }
        Task AuthenticateUser(string password);
        bool IsAuthenticated { get; }
    }
}
