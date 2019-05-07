using System.Threading.Tasks;

namespace AspNetCoreDemo.Security
{
    public interface IUserService
    {
        Task<bool> Authenticate(string userName, string password);
    }
}
