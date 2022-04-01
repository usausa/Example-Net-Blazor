namespace AuthenticationExample.Components.Authentication;

using System.Diagnostics.CodeAnalysis;

public class CustomAuthenticationSetting
{
    [AllowNull]
    public string SecretKey { get; set; }

    public int Expire { get; set; }
}
