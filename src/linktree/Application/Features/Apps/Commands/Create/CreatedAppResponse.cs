using Domain.Entities;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.Apps.Commands.Create;

public class CreatedAppResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public int CompanyId { get; set; }
}
