namespace FantasyFootball.Application.UseCases.FantasyTeams.Commands.DeleteFantasyTeam
{
    public class DeleteFantasyTeamCommandValidator : AbstractValidator<DeleteFantasyTeamCommand>
    {
        public DeleteFantasyTeamCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Fantasy Team Id is required.");
        }
    }
}
