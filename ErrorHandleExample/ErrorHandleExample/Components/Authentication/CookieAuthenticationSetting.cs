namespace ErrorHandleExample.Components.Authentication;

public class CookieAuthenticationSetting
{
    public string SecretKey { get; set; } = default!;

    public string Issuer { get; set; } = default!;

    public int Expire { get; set; }
}
