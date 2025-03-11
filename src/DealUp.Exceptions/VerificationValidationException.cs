using System.Net;

namespace DealUp.Exceptions;

public class VerificationValidationException() : ResponseErrorException("The verification token is invalid or expired, or there are no pending verification requests for this account.")
{
    public override HttpStatusCode ResponseStatusCode => HttpStatusCode.Forbidden;
}