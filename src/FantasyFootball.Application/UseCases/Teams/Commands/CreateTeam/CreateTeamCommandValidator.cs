namespace FantasyFootball.Application.UseCases.Teams.Commands.CreateTeam
{
    public class CreateTeamCommandValidator : AbstractValidator<CreateTeamCommand>
    {
        public CreateTeamCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Team name is required.");

            RuleFor(x => x.ShortName)
                .NotEmpty()
                .WithMessage("Team short name is required.");
        }
    }
}
