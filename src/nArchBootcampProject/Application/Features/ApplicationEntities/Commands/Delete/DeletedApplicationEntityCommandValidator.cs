using FluentValidation;

namespace Application.Features.ApplicationEntities.Commands.Delete;

public class DeleteApplicationEntityCommandValidator : AbstractValidator<DeleteApplicationEntityCommand>
{
    public DeleteApplicationEntityCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
