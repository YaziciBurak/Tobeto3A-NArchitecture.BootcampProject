using FluentValidation;

namespace Application.Features.ApplicationEntities.Commands.Update;

public class UpdateApplicationEntityCommandValidator : AbstractValidator<UpdateApplicationEntityCommand>
{
    public UpdateApplicationEntityCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.ApplicantId).NotEmpty();
        RuleFor(c => c.BootcampId).NotEmpty();
        RuleFor(c => c.ApplicationStateId).NotEmpty();
    }
}
