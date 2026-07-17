namespace FantasyFootball.Application.UseCases.FantasyTeams.Commands.CreateFantasyTeam
{
    public class CreateFantasyTeamCommandValidator : AbstractValidator<CreateFantasyTeamCommand>
    {
        public CreateFantasyTeamCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Fantasy team name is required.");

            RuleFor(x => x.ManagerId)
                .NotEmpty()
                .WithMessage("Manager Id is required.");
        }
    }
}
