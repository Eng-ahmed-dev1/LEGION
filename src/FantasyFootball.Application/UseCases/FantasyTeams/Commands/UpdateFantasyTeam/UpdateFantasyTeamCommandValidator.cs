namespace FantasyFootball.Application.UseCases.FantasyTeams.Commands.UpdateFantasyTeam
{
    public class UpdateFantasyTeamCommandValidator : AbstractValidator<UpdateFantasyTeamCommand>
    {
        public UpdateFantasyTeamCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Fantasy Team Id is required.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Fantasy Team name is required.");

            RuleFor(x => x.Budget)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Budget must be non-negative.");

            RuleFor(x => x.FreeTransfers)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Free transfers must be non-negative.");
        }
    }
}
