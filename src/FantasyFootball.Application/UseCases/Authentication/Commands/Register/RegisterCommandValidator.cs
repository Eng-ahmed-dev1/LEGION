namespace FantasyFootball.Application.UseCases.Authentication.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(v => v.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email must be a valid email address.");

        RuleFor(v => v.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters.");

        RuleFor(v => v.TeamName)
            .NotEmpty().WithMessage("Team Name is required.");

        RuleFor(v => v.Username)
            .NotEmpty().WithMessage("Username is required.");
    }
}
