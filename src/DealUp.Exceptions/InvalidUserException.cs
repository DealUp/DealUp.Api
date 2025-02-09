using System.Net;

namespace DealUp.Exceptions;

public class InvalidUserException(string message) : ResponseErrorException(message)
{
    public override HttpStatusCode ResponseStatusCode => HttpStatusCode.BadRequest;
}