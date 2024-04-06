using Domain.Entities;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.Apps.Queries.GetById;

public class GetByIdAppResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string PlayStoreUrl { get; set; }
    public string AppStoreUrl { get; set; }
}