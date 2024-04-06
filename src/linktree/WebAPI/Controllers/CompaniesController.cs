using Application.Features.Companies.Commands.Create;
using Application.Features.Companies.Commands.Delete;
using Application.Features.Companies.Commands.Update;
using Application.Features.Companies.Commands.UpdateCompanyImage;
using Application.Features.Companies.Queries.GetById;
using Application.Features.Companies.Queries.GetList;
using Microsoft.AspNetCore.Mvc;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompaniesController : BaseController
{
    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Add([FromForm] string name, [FromForm] string description, IFormFile image)
    {
        var createCompanyCommand = new CreateCompanyCommand
        {
            Name = name,
            Description = description,
            ImageUrl = image
        };

        CreatedCompanyResponse response = await Mediator.Send(createCompanyCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateCompanyCommand command)
    {
        UpdatedCompanyResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedCompanyResponse response = await Mediator.Send(new DeleteCompanyCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdCompanyResponse response = await Mediator.Send(new GetByIdCompanyQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListCompanyQuery getListCompanyQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListCompanyListItemDto> response = await Mediator.Send(getListCompanyQuery);
        return Ok(response);
    }

    [HttpPut("UpdateCompanyImage")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UpdateCompanyImage([FromForm] int id, IFormFile file)
    {
        UpdateCompanyImageCommand updateCompanyImageCommand = new() { Id = id, Image = file };
        UpdateCompanyImageResponse response = await Mediator.Send(updateCompanyImageCommand);
        return Ok(response);
    }
}
