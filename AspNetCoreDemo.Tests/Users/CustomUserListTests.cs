using System;
using System.Collections.Generic;
using System.Text;
using AspNetCoreDemo.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AspNetCoreDemo.Tests.Users
{
    [TestClass]
    public class CustomUserListTests
    {
        [TestMethod]
        public void UserDoesNotExistReturnsNull()
        {
            var userList = new CustomUserList();

            Assert.IsNull(userList.GetUser("doesNotExist"));
        }

        [TestMethod]
        public void LsmUserExists()
        {
            var userList = new CustomUserList();

            Assert.IsNotNull(userList.GetUser(LsmUser.LsmUsername));
        }
    }
}
