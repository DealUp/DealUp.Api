using System.Net;

namespace DealUp.Exceptions;

public class MustCreateSellerProfileException(Guid userId) : ResponseErrorException($"User '{userId}' must create a seller profile before performing this action.")
{
    public override HttpStatusCode ResponseStatusCode => HttpStatusCode.Forbidden;
}