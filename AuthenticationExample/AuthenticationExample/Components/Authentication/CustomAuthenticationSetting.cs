namespace AuthenticationExample.Components.Authentication;

public class CustomAuthenticationSetting
{
    public string SecretKey { get; set; } = default!;

    public string Issuer { get; set; } = default!;

    public int Expire { get; set; }
}
