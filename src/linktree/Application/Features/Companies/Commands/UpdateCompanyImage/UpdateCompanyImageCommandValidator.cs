using FluentValidation;

namespace Application.Features.Companies.Commands.UpdateCompanyImage;

public class UpdateCompanyImageCommandValidator : AbstractValidator<UpdateCompanyImageCommand>
{
    public UpdateCompanyImageCommandValidator()
    {
        RuleFor(i => i.Id).NotEmpty();
        RuleFor(i => i.Image).NotEmpty();
    }
}