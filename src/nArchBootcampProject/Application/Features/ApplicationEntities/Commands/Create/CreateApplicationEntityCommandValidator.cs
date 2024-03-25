using FluentValidation;

namespace Application.Features.ApplicationEntities.Commands.Create;

public class CreateApplicationEntityCommandValidator : AbstractValidator<CreateApplicationEntityCommand>
{
    public CreateApplicationEntityCommandValidator()
    {
        RuleFor(c => c.ApplicantId).NotEmpty();
        RuleFor(c => c.BootcampId).NotEmpty();
        RuleFor(c => c.ApplicationStateId).NotEmpty();
    }
}
