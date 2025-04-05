using System.Net;

namespace DealUp.Exceptions;

public abstract class ResponseErrorException : Exception
{
    public abstract HttpStatusCode ResponseStatusCode { get; }
    public virtual object? AdditionalData { get; }

    protected ResponseErrorException(string? message) : base(message)
    {
        
    }

    protected ResponseErrorException(string? message, object? additionalData) : this(message)
    {
        AdditionalData = additionalData;
    }

    protected ResponseErrorException(string? message, Exception? innerException, object? additionalData) : base(message, innerException)
    {
        AdditionalData = additionalData;
    }
}