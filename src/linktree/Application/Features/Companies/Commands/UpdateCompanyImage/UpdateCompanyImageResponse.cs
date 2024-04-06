using NArchitecture.Core.Application.Responses;

namespace Application.Features.Companies.Commands.UpdateCompanyImage;

public class UpdateCompanyImageResponse : IResponse
{
    public int Id { get; set; }
    public string ImageUrl { get; set; }
}
