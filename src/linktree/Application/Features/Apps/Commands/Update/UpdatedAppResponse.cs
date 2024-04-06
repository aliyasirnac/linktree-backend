using Domain.Entities;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.Apps.Commands.Update;

public class UpdatedAppResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public int CompanyId { get; set; }
    public Company Company { get; set; }
}
