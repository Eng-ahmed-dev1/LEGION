namespace FantasyFootball.Application.UseCases.Teams.Commands.DeleteTeam
{
    public class DeleteTeamCommandValidator : AbstractValidator<DeleteTeamCommand>
    {
        public DeleteTeamCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Team Id is required.");
        }
    }
}
