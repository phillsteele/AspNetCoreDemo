using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreDemo.Security
{
    public interface IUserList
    {
        IUser GetUser(string userName);
    }
}
