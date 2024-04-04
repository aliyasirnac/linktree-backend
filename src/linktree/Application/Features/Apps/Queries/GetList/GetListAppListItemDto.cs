using Domain.Entities;
using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Apps.Queries.GetList;

public class GetListAppListItemDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string PlayStoreUrl { get; set; }
    public string AppStoreUrl { get; set; }
    public int CompanyId { get; set; }
    public Company Company { get; set; }
}