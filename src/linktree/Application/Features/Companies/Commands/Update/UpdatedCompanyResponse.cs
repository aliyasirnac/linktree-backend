using NArchitecture.Core.Application.Responses;

namespace Application.Features.Companies.Commands.Update;

public class UpdatedCompanyResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string PlayStoreUrl { get; set; }
    public string AppStoreUrl { get; set; }
}
