using NArchitecture.Core.Application.Responses;

namespace Application.Features.Companies.Commands.Create;

public class CreatedCompanyResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
}
