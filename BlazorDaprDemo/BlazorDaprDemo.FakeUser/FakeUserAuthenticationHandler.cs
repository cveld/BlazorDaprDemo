using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace BlazorDaprDemo.FakeUser
{
    public class FakeUserAuthenticationHandler : AuthenticationHandler<FakeUserOptions>
    {
        public FakeUserAuthenticationHandler(
          IOptionsMonitor<FakeUserOptions> options,
          ILoggerFactory logger,
          UrlEncoder encoder,
          ISystemClock clock)
          : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var claims = new[] {
                    new Claim(ClaimTypes.NameIdentifier, "TEST"),
                    new Claim(ClaimTypes.Email, "test@test.nl"),
                    new Claim(ClaimTypes.Name, "testname") };

            // generate claimsIdentity on the name of the class
            var claimsIdentity = new ClaimsIdentity(claims,
                        nameof(FakeUserAuthenticationHandler));

            // generate AuthenticationTicket from the Identity
            // and current authentication scheme
            var ticket = new AuthenticationTicket(
                new ClaimsPrincipal(claimsIdentity), this.Scheme.Name);

            // pass on the ticket to the middleware
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
