using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace AspNetCoreDemo.Security
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserService _userService;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IUserService userService)
            : base(options, logger, encoder, clock)
        {
            _userService = userService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            return await BasicAuthenticationHandlerCore.HandleAuthenticateAsync(Request, Scheme, _userService);
        }
    }

    // Moved core logic into a seperate class to make it easier to unit test
    public static class BasicAuthenticationHandlerCore
    {
        private const string Authorization = "Authorization";

        public static async Task<AuthenticateResult> HandleAuthenticateAsync(HttpRequest request, AuthenticationScheme scheme, IUserService userService)
        {
            if (!request.Headers.ContainsKey(Authorization))
                return AuthenticateResult.Fail("Missing Authorization Header");

            string userName = "";
            bool authenticated = false;

            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(request.Headers[Authorization]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
                userName = credentials[0];
                var password = credentials[1];
                authenticated = await userService.Authenticate(userName, password);
            }
            catch
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

            if (!authenticated)
                return AuthenticateResult.Fail("Invalid Username or Password");

            var claims = new[] { new Claim(ClaimTypes.Name, userName) };

            var identity = new ClaimsIdentity(claims, scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
