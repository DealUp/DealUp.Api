using DealUp.DataLake.AmazonS3.Models;
using DealUp.DataLake.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DealUp.Application.Api.Controllers.v1.Media;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/v1/media")]
public class MediaController(IDataLake dataLake) : ControllerBase
{
    [HttpGet("{sessionId:guid}")]
    public ActionResult<CreatePreSignedPostResponse> GeneratePreSignedPost([FromRoute] Guid sessionId)
    {
        var response = dataLake.GeneratePreSignedPostAsync(sessionId.ToString());
        return Ok(response);
    }

    [HttpGet("{mediaKey:required}")]
    public async Task<ActionResult<string>> GeneratePreSignedGet([FromRoute] string mediaKey)
    {
        var mediaUrl = await dataLake.GeneratePreSignedGetAsync(Uri.UnescapeDataString(mediaKey));
        return Redirect(mediaUrl);
    }
}