using DealUp.Domain.User.Values;
using DealUp.Infrastructure.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DealUp.Infrastructure.Attributes;

public class AuthorizeForAttribute : TypeFilterAttribute
{
    public AuthorizeForAttribute(params UserVerificationStatus[] allowedStatuses) : base(typeof(UserStatusAuthorizationFilter))
    {
        Arguments = [allowedStatuses];
    }
}