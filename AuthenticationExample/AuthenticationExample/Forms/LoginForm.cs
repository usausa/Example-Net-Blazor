namespace AuthenticationExample.Forms;

public class LoginForm
{
    [Required]
    [AllowNull]
    public string Username { get; set; }

    [Required]
    [AllowNull]
    public string Password { get; set; }
}
