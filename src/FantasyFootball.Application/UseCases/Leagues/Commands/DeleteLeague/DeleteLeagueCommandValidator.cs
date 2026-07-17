namespace FantasyFootball.Application.UseCases.Leagues.Commands.DeleteLeague
{
    public class DeleteLeagueCommandValidator : AbstractValidator<DeleteLeagueCommand>
    {
        public DeleteLeagueCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("League Id is required.");
        }
    }
}
