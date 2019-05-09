using AspNetCoreDemo.Security;
using System.Collections.Generic;

namespace AspNetCoreDemo.Users
{
    public class CustomUserList : IUserList
    {
        private Dictionary<string, IUser> _userList = new Dictionary<string, IUser>();

        public CustomUserList()
        {
            PopulateUserList();
        }

        private void PopulateUserList()
        {
            _userList.Add(LsmUser.LsmUsername, new LsmUser());
        }

        public IUser GetUser(string userName)
        {
            return _userList.ContainsKey(userName) ?
                _userList[userName] :
                null;
        }
    }
}
