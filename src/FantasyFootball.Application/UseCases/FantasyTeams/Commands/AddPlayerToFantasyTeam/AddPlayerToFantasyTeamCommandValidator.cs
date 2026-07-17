namespace FantasyFootball.Application.UseCases.FantasyTeams.Commands.AddPlayerToFantasyTeam
{
    public class AddPlayerToFantasyTeamCommandValidator : AbstractValidator<AddPlayerToFantasyTeamCommand>
    {
        public AddPlayerToFantasyTeamCommandValidator()
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
