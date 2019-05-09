using System;
using System.Threading.Tasks;

namespace AspNetCoreDemo.Security
{
    public class SimpleUserService : IUserService
    {
        private IUserList _userList;

        public SimpleUserService(IUserList userList)
        {
            _userList = userList ?? throw new ArgumentNullException(nameof(userList));
        }

        public async Task<IUser> Authenticate(string userName, string password)
        {
            var user = _userList.GetUser(userName);

            // Does user exist?
            if (user == null)
            {
                return null;
            }

            // Is the password correct
            await user.AuthenticateUser(password);

            // Password was wrong
            return user;
        }
    }
}
