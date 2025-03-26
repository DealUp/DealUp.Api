using System.Net;

namespace DealUp.Exceptions;

public class EntityAlreadyExistsException(string entityName) : ResponseErrorException($"Entity '{entityName}' already exists.")
{
    public override HttpStatusCode ResponseStatusCode => HttpStatusCode.Conflict;
}