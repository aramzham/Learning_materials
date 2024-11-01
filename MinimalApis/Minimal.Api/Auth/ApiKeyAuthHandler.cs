using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace Minimal.Api.Auth;

public class ApiKeyAuthHandler : AuthenticationHandler<ApiKeyAuthSchemeOptions>
{
    public ApiKeyAuthHandler(IOptionsMonitor<ApiKeyAuthSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    public ApiKeyAuthHandler(IOptionsMonitor<ApiKeyAuthSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder) : base(options, logger, encoder)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey(HeaderNames.Authorization))
            return Task.FromResult(AuthenticateResult.Fail("Invalid api key"));

        var header = Request.Headers[HeaderNames.Authorization].ToString();
        if (header != Options.ApiKey)
            return Task.FromResult(AuthenticateResult.Fail("Invalid api key"));

        var claims = new[]
        {
            new Claim(ClaimTypes.Email, "andy@roddick.us"),
            new Claim(ClaimTypes.Name, "andy@roddick.us")
        };

        var claimsIdentity = new ClaimsIdentity(claims, "ApiKey");

        var ticket = new AuthenticationTicket(
            new ClaimsPrincipal(claimsIdentity), Scheme.Name
            );

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}