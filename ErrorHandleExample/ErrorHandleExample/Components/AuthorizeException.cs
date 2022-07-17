namespace ErrorHandleExample.Components;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors", Justification = "Ignore")]
public class AuthorizeException : Exception
{
    public int StatusCode { get; }

    public AuthorizeException(int statusCode)
    {
        StatusCode = statusCode;
    }
}

[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors", Justification = "Ignore")]
public sealed class NotFoundException : AuthorizeException
{
    public NotFoundException()
        : base(StatusCodes.Status404NotFound)
    {
    }
}

[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors", Justification = "Ignore")]
public sealed class ForbiddenException : AuthorizeException
{
    public ForbiddenException()
        : base(StatusCodes.Status403Forbidden)
    {
    }
}
