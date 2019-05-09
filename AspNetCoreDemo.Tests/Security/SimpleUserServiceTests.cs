using System;
using System.Collections.Generic;
using System.Text;
using AspNetCoreDemo.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AspNetCoreDemo.Tests.Security
{
    [TestClass]
    public class SimpleUserServiceTests
    {
        [TestMethod]
        public void CanCreateSiampleUserService()
        {
            var mockUserList = new Mock<IUserList>();
            var userService = new SimpleUserService(mockUserList.Object);
            Assert.IsNotNull(userService);
        }
    }
}
