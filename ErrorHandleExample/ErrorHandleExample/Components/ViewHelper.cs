namespace ErrorHandleExample.Components;

using MudBlazor;

public static class ViewHelper
{
    public static InputType PasswordInputType(bool visible)
    {
        return visible ? InputType.Text : InputType.Password;
    }

    public static string PasswordInputIcon(bool visible)
    {
        return visible ? Icons.Material.Filled.VisibilityOff : Icons.Material.Filled.Visibility;
    }
}
