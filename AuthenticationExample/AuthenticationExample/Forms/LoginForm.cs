namespace AuthenticationExample.Forms;

public class LoginForm
{
    [Required]
    public string Username { get; set; } = default!;

    [Required]
    public string Password { get; set; } = default!;
}
