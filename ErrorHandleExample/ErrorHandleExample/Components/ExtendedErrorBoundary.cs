namespace ErrorHandleExample.Components;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

public class ExtendedErrorBoundary : ErrorBoundary
{
    [Inject]
    public ILogger<ExtendedErrorBoundary> Logger { get; set; } = default!;

    protected override async Task OnErrorAsync(Exception exception)
    {
        await base.OnErrorAsync(exception);

        Logger.LogError(exception, "Unknown exception.");
    }
}
