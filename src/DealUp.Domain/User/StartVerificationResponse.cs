namespace DealUp.Domain.User;

public class StartVerificationResponse
{
    public bool Success { get; private set; }
    public string Message { get; private set; }

    private StartVerificationResponse(bool success, string message)
    {
        Success = success;
        Message = message;
    }

    public static StartVerificationResponse CreateUnsuccessful(string message)
    {
        return new StartVerificationResponse(false, message);
    }

    public static StartVerificationResponse CreateSuccessful(string message)
    {
        return new StartVerificationResponse(true, message);
    }
}