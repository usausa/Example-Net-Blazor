namespace AuthenticationExample.Components.Authentication;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.JSInterop;

using Smart.Text;

public sealed class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private const string AccountKey = "__account";

    private static readonly DateTime ExpireDate = new(1970, 1, 1);

    private static readonly ClaimsPrincipal Anonymous = new();

    private readonly IHttpContextAccessor httpContextAccessor;

    private readonly IJSRuntime jsRuntime;

    private readonly CustomAuthenticationSetting setting;

    private readonly byte[] secretKey;

    private ClaimsPrincipal? cachedPrincipal;

    public CustomAuthenticationStateProvider(IHttpContextAccessor httpContextAccessor, IJSRuntime jsRuntime, IOptions<CustomAuthenticationSetting> setting)
    {
        this.httpContextAccessor = httpContextAccessor;
        this.jsRuntime = jsRuntime;
        this.setting = setting.Value;
        secretKey = HexEncoder.Decode(setting.Value.SecretKey);
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if (cachedPrincipal is not null)
        {
            return Task.FromResult(new AuthenticationState(cachedPrincipal));
        }

        var principal = LoadAccount();
        if (principal is not null)
        {
            cachedPrincipal = principal;
            return Task.FromResult(new AuthenticationState(principal));
        }

        return Task.FromResult(new AuthenticationState(Anonymous));
    }

    public async Task LoginAsync(string username, string password)
    {
        // TODO
        if (username != password)
        {
            return;
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, username)
        };
        if (username == "admin")
        {
            claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
        }

        var identify = new ClaimsIdentity(claims, "custom");
        await SaveAccountAsync(identify).ConfigureAwait(false);

        cachedPrincipal = new ClaimsPrincipal(identify);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(cachedPrincipal)));
    }

    public async Task LogoutAsync()
    {
        cachedPrincipal = null;
        await UpdateCookie(string.Empty, ExpireDate).ConfigureAwait(false);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(Anonymous)));
    }

    private async Task SaveAccountAsync(ClaimsIdentity identity)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = identity,
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var value = tokenHandler.WriteToken(token);

        await UpdateCookie(value, DateTime.Now.AddMinutes(setting.Expire)).ConfigureAwait(false);
    }

    private ClaimsPrincipal? LoadAccount()
    {
        var value = httpContextAccessor.HttpContext?.Request.Cookies[AccountKey];
        if (String.IsNullOrEmpty(value))
        {
            return null;
        }

        try
        {
            var parameter = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                ValidateAudience = false,
                ValidateIssuer = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(value, parameter, out var validatedToken);
            if (validatedToken.ValidTo < DateTime.UtcNow)
            {
                return null;
            }

            return principal;
        }
        catch (SecurityTokenException)
        {
            return null;
        }
    }

    private async Task UpdateCookie(string value, DateTime expire)
    {
        await jsRuntime.InvokeVoidAsync("eval", $"document.cookie = \"{AccountKey}={value}; expires={expire.ToUniversalTime():R}\"").ConfigureAwait(false);
    }
}
