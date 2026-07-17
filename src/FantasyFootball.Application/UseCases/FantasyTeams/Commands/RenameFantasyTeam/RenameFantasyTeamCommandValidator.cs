namespace FantasyFootball.Application.UseCases.FantasyTeams.Commands.RenameFantasyTeam
{
    public class RenameFantasyTeamCommandValidator : AbstractValidator<RenameFantasyTeamCommand>
    {
        public RenameFantasyTeamCommandValidator()
        {
            RuleFor(x => x.id)
                .NotEmpty()
                .WithMessage("id is required.");

            RuleFor(x => x.name)
                .NotEmpty()
                .WithMessage("name is required.");
        }
    }
}
