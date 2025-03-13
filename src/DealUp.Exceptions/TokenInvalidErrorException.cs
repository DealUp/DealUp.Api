using System.Net;

namespace DealUp.Exceptions;

public class TokenInvalidErrorException(Exception? validationFailure) : ResponseErrorException("The token is not valid or expired.", validationFailure)
{
    public override HttpStatusCode ResponseStatusCode => HttpStatusCode.Unauthorized;
}