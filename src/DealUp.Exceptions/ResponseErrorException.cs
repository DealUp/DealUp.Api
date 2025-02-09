using System.Net;

namespace DealUp.Exceptions;

public abstract class ResponseErrorException : Exception
{
    public abstract HttpStatusCode ResponseStatusCode { get; }
    public virtual object? AdditionalData { get; }

    protected ResponseErrorException(object? additionalData)
    {
        AdditionalData = additionalData;
    }

    protected ResponseErrorException(string? message, object? additionalData) : base(message)
    {
        AdditionalData = additionalData;
    }

    protected ResponseErrorException(string? message, Exception? innerException, object? additionalData) : base(message, innerException)
    {
        AdditionalData = additionalData;
    }
}