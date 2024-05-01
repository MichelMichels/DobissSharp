namespace MichelMichels.DobissSharp.Api.Exceptions;

public class DobissSharpApiException : Exception
{
    public DobissSharpApiException()
    {
    }

    public DobissSharpApiException(string? message) : base(message)
    {
    }

    public DobissSharpApiException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
