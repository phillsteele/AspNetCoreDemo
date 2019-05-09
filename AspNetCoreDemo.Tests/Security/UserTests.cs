using AspNetCoreDemo.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNetCoreDemo.Tests.Security
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void CanCreateUser()
        {
            var user = new User("bob");
            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void DefaultUsernameClaimAdded()
        {
            var user = new User("bob");
            Assert.AreEqual("bob", user.Claims[ClaimTypes.Name]);
        }

        [TestMethod]
        public void OneDefaultClaimAdded()
        {
            var user = new User("bob");
            Assert.AreEqual(1, user.Claims.Count);
        }

        [TestMethod]
        public async Task ValidPasswordAuthenticates()
        {
            var user = new User("bob");
            await user.AuthenticateUser("123");
            Assert.IsTrue(user.IsAuthenticated);
        }
        [TestMethod]
        public async Task InvalidPassword_FailsToAuthenticates()
        {
            var user = new User("bob");
            await user.AuthenticateUser("1234");
            Assert.IsFalse(user.IsAuthenticated);
        }

    }
}
