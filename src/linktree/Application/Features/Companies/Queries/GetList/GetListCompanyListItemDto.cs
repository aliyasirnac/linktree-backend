using Domain.Entities;
using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Companies.Queries.GetList;

public class GetListCompanyListItemDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public virtual ICollection<App> Apps { get; set; }
}
