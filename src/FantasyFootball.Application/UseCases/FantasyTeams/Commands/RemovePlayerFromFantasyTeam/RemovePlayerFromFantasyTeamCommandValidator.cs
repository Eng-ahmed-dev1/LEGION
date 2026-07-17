namespace FantasyFootball.Application.UseCases.FantasyTeams.Commands.RemovePlayerFromFantasyTeam
{
    public class RemovePlayerFromFantasyTeamCommandValidator : AbstractValidator<RemovePlayerFromFantasyTeamCommand>
    {
        public RemovePlayerFromFantasyTeamCommandValidator()
        {
            RuleFor(x => x.FantasyTeamId)
                .NotEmpty()
                .WithMessage("FantasyTeamId is required.");

            RuleFor(x => x.PlayerId)
                .NotEmpty()
                .WithMessage("PlayerId is required.");
        }
    }
}
