using NArchitecture.Core.Application.Responses;

namespace Application.Features.Apps.Commands.Delete;

public class DeletedAppResponse : IResponse
{
    public int Id { get; set; }
}
