namespace DealUp.Infrastructure;

public class ExceptionData(int statusCode, string message, string? details, object? additionalData = null)
{
    public ExceptionData(int statusCode, string message, object? additionalData = null) : this(statusCode, message, null, additionalData)
    {
    }
    
    public int StatusCode { get; set; } = statusCode;
    public string Message { get; set; } = message;
    public string? Details { get; set; } = details;
    public object? AdditionalData { get; set; } = additionalData;
}