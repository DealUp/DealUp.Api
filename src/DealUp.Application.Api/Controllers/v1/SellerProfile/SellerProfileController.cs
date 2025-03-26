using DealUp.Domain.Identity.Interfaces;
using DealUp.Domain.Seller.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DealUp.Application.Api.Controllers.v1.SellerProfile;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/v1/seller-profile")]
public class SellerProfileController(IHttpContextService httpContextService, ISellerService sellerService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateSellerProfile()
    {
        var userId = httpContextService.GetUserIdOrThrow();
        await sellerService.CreateSellerProfileAsync(userId);
        return NoContent();
    }
}