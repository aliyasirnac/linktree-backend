using FluentValidation;

namespace Application.Features.Apps.Commands.Update;

public class UpdateAppCommandValidator : AbstractValidator<UpdateAppCommand>
{
    public UpdateAppCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.ImageUrl).NotEmpty();
        RuleFor(c => c.CompanyId).NotEmpty();
    }
}