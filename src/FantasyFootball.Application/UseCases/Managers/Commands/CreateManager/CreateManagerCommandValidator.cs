namespace FantasyFootball.Application.UseCases.Managers.Commands.CreateManager
{
    public class CreateManagerCommandValidator : AbstractValidator<CreateManagerCommand>
    {
        public CreateManagerCommandValidator()
        {
            RuleFor(x => x.TeamName)
                .NotEmpty()
                .WithMessage("Team name is required.");

            RuleFor(x => x.ApplicationUserId)
                .NotEmpty()
                .WithMessage("Application User Id is required.");

            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("User name is required.");
        }
    }
}
