using FluentValidation;

namespace Application.Features.Apps.Commands.Delete;

public class DeleteAppCommandValidator : AbstractValidator<DeleteAppCommand>
{
    public DeleteAppCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
