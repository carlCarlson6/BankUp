using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BankUp.Backend.Groups;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BankUp.Backend.Test.Api;

public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public TestAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock)
        : base(options, logger, encoder, clock) { }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        Context.Request.Headers.TryGetValue("user", out var userIdString);
        var user = TestUsers.All.FirstOrDefault(u => u.Id.ToString() == userIdString.ToString());

        return user switch
        {
            null => Task.FromResult(AuthenticateResult.NoResult()),
            {} => OnFoundUser(user)
        };
    }

    private static Task<AuthenticateResult> OnFoundUser(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
        };
        var identity = new ClaimsIdentity(claims, "Bearer");
        
        var principal = new ClaimsPrincipal(identity);
        
        var ticket = new AuthenticationTicket(principal, "Bearer");
        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}