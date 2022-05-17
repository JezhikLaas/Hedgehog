using FluentValidation;

namespace Hedgehog.Persistence.Actors;

public class RoleValidator : AbstractValidator<Role>
{
    private RoleValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();
        RuleFor(x => x.Description)
            .NotEmpty();
    }

    public static readonly RoleValidator Instance = new();
}