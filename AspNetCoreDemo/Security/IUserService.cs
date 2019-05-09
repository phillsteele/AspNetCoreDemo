using System.Threading.Tasks;

namespace AspNetCoreDemo.Security
{
    public interface IUserService
    {
        Task<IUser> Authenticate(string userName, string password);
    }
}
