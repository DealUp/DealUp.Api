namespace DealUp.Domain.Identity.Interfaces;

public interface IHttpContextService
{
    public Guid GetUserIdOrThrow();
}
