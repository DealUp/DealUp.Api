using DealUp.Domain.User;
using DealUp.Infrastructure.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DealUp.Infrastructure.Attributes;

public class AuthorizeForAttribute : TypeFilterAttribute
{
    public AuthorizeForAttribute(params Status[] allowedStatuses) : base(typeof(UserStatusAuthorizationFilter))
    {
        Arguments = [allowedStatuses];
    }
}