using System.ComponentModel.DataAnnotations;
using DealUp.Application.Api.Controllers.Common;
using DealUp.Domain.Advertisement.Interfaces;
using DealUp.Domain.Identity.Interfaces;
using DealUp.Domain.User.Values;
using DealUp.Dto.Common;
using DealUp.Dto.v1.Advertisement;
using DealUp.Infrastructure.Attributes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DealUp.Application.Api.Controllers.v1.Advertisement;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/v1/advertisements")]
public class AdvertisementController(IHttpContextService httpContextService, IAdvertisementService advertisementService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<AdvertisementSummaryDto>>> GetAllAdvertisements([FromQuery] PaginationParametersDto pagination)
    {
        var advertisements = await advertisementService.GetAllAdvertisementsAsync(pagination.ToDomain());
        return Ok(advertisements.ToPagedDto(Converter.ToEnumerableDto));
    }

    [AuthorizeFor(UserVerificationStatus.Confirmed)]
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateAdvertisement([FromBody, Required] CreateAdvertisementDto createAdvertisement)
    {
        var userId = httpContextService.GetUserIdOrThrow();
        var advertisementId = await advertisementService.CreateAdvertisementAsync(userId, createAdvertisement.ToDomain());
        return StatusCode(StatusCodes.Status201Created, advertisementId);
    }
}