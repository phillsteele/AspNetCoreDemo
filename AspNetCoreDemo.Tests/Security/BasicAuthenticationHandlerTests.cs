using AspNetCoreDemo.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace AspNetCoreDemo.Tests.Security
{
    [TestClass]
    public class BasicAuthenticationHandlerTests
    {
        [TestMethod]
        public async Task MissingAuthorizationHeader_ExpectFailure()
        {
            var httpContext = new DefaultHttpContext();
            var httpRequest = httpContext.Request;
            var scheme = new AuthenticationScheme("BasicAuthorisation", "BasicAuthorisation", typeof(BasicAuthenticationHandler));
            var userService = new Mock<IUserService>();

            var authenticateResult = await BasicAuthenticationHandlerCore.HandleAuthenticateAsync(httpRequest, scheme, userService.Object);

            Assert.IsNotNull(authenticateResult.Failure);
            Assert.IsFalse(authenticateResult.Succeeded);
        }

        [TestMethod]
        public async Task AuthorizationHeaderPresentWithNoCredentials_ExpectFailure()
        {
            var httpContext = new DefaultHttpContext();
            var httpRequest = httpContext.Request;
            var scheme = new AuthenticationScheme("BasicAuthorisation", "BasicAuthorisation", typeof(BasicAuthenticationHandler));
            var userService = new Mock<IUserService>();

            // Add authorization header
            httpRequest.Headers.Add("Authorization", "");

            var authenticateResult = await BasicAuthenticationHandlerCore.HandleAuthenticateAsync(httpRequest, scheme, userService.Object);

            Assert.IsNotNull(authenticateResult.Failure);
            Assert.IsFalse(authenticateResult.Succeeded);
        }

        [TestMethod]
        public async Task AuthorizationHeaderPresentWithInvalidCredentials_ExpectFailure()
        {
            var httpContext = new DefaultHttpContext();
            var httpRequest = httpContext.Request;
            var scheme = new AuthenticationScheme("BasicAuthorisation", "BasicAuthorisation", typeof(BasicAuthenticationHandler));
            var userService = new Mock<IUserService>();

            // Add authorization header - username = abc  password = xyz
            httpRequest.Headers.Add("Authorization", "Basic YWJjOnh5eg==");

            var authenticateResult = await BasicAuthenticationHandlerCore.HandleAuthenticateAsync(httpRequest, scheme, userService.Object);

            Assert.IsNotNull(authenticateResult.Failure);
            Assert.IsFalse(authenticateResult.Succeeded);
        }

        [TestMethod]
        public async Task AuthorizationHeaderPresentWithValidCredentials_ExpectSuccess()
        {
            var httpContext = new DefaultHttpContext();
            var httpRequest = httpContext.Request;
            var scheme = new AuthenticationScheme("BasicAuthorisation", "BasicAuthorisation", typeof(BasicAuthenticationHandler));
            var userService = new Mock<IUserService>();
            userService.Setup(t => t.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns(async () => true);

            // Add authorization header - username = abc  password = xyz
            httpRequest.Headers.Add("Authorization", "Basic YWJjOnh5eg==");

            var authenticateResult = await BasicAuthenticationHandlerCore.HandleAuthenticateAsync(httpRequest, scheme, userService.Object);

            Assert.IsNull(authenticateResult.Failure);
            Assert.IsTrue(authenticateResult.Succeeded);
        }

        [TestMethod]
        public async Task ExpectUserNameAndPasswordToBePassedToUserService()
        {
            var httpContext = new DefaultHttpContext();
            var httpRequest = httpContext.Request;
            var scheme = new AuthenticationScheme("BasicAuthorisation", "BasicAuthorisation", typeof(BasicAuthenticationHandler));
            var userService = new Mock<IUserService>();
            userService.Setup(t => t.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns(async () => true);

            // Add authorization header - username = abc  password = xyz
            httpRequest.Headers.Add("Authorization", "Basic YWJjOnh5eg==");

            var authenticateResult = await BasicAuthenticationHandlerCore.HandleAuthenticateAsync(httpRequest, scheme, userService.Object);

            // Exception thrown if this condition has not been met
            userService.Verify(t => t.Authenticate("abc", "xyz"));
        }
    }
}
