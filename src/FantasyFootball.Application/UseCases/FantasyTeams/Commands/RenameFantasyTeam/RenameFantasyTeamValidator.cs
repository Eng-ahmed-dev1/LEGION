namespace FantasyFootball.Application.UseCases.FantasyTeams.Commands.RenameFantasyTeam
{
    public class RenameFantasyTeamValidator : AbstractValidator<RenameFantasyTeamCommand>
    {
        public RenameFantasyTeamValidator()
        {
            RuleFor(x => x.id)
            .NotEmpty()
            .WithMessage("The Id of the fantasyteam must be not empty");

            RuleFor(x => x.name)
            .NotEmpty()
            .WithMessage("The name of the fantasyteam must be not empty");
        }
    }
}