using Application.Features.Apps.Commands.Create;
using Application.Features.Apps.Commands.Delete;
using Application.Features.Apps.Commands.Update;
using Application.Features.Apps.Commands.UpdateImageApp;
using Application.Features.Apps.Queries.GetById;
using Application.Features.Apps.Queries.GetList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppsController : BaseController
{
    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Add(
        [FromForm] string name,
        [FromForm] int companyId,
        [FromForm] string playStoreUrl,
        [FromForm] string appStoreUrl,
        [FromForm] IFormFile file
    )
    {
        CreateAppCommand createAppCommand =
            new()
            {
                Name = name,
                ImageUrl = file,
                CompanyId = companyId,
                PlayStoreUrl = playStoreUrl,
                AppStoreUrl = appStoreUrl
            };
        CreatedAppResponse response = await Mediator.Send(createAppCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Update(
        [FromForm] int id,
        [FromForm] string name,
        [FromForm] int companyId,
        [FromForm] string playStoreUrl,
        [FromForm] string appStoreUrl,
        [FromForm] IFormFile file
    )
    {
        UpdateAppCommand updateAppCommand =
            new()
            {
                Id = id,
                Name = name,
                ImageUrl = file,
                PlayStoreUrl = playStoreUrl,
                AppStoreUrl = appStoreUrl,
                CompanyId = companyId
            };
        UpdatedAppResponse response = await Mediator.Send(updateAppCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedAppResponse response = await Mediator.Send(new DeleteAppCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdAppResponse response = await Mediator.Send(new GetByIdAppQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListAppQuery getListAppQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListAppListItemDto> response = await Mediator.Send(getListAppQuery);
        return Ok(response);
    }

    [HttpPut("UpdateImageApp")]
    public async Task<IActionResult> UpdateImageApp([FromBody] UpdateImageAppCommand updateImageAppCommand)
    {
        UpdateImageAppResponse response = await Mediator.Send(updateImageAppCommand);
        return Ok(response);
    }
}
