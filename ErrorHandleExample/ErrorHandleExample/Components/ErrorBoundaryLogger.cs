namespace ErrorHandleExample.Components;

using Microsoft.AspNetCore.Components.Web;

public sealed class ErrorBoundaryLogger : IErrorBoundaryLogger
{
    private readonly ILogger<ErrorBoundaryLogger> log;

    public ErrorBoundaryLogger(ILogger<ErrorBoundaryLogger> log)
    {
        this.log = log;
    }

    public ValueTask LogErrorAsync(Exception exception)
    {
        log.LogError(exception, "Unknown exception.");
        return ValueTask.CompletedTask;
    }
}
