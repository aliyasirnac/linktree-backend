using Application.Features.Apps.Queries.GetById;
using Domain.Entities;
using NArchitecture.Core.Application.Responses;
using System.Text.Json.Serialization;

namespace Application.Features.Companies.Queries.GetById;

public class GetByIdCompanyResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    // [JsonIgnore]
    public virtual ICollection<GetByIdAppResponse> Apps { get; set; }
}