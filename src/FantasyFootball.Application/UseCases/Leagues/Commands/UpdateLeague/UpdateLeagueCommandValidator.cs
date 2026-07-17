namespace FantasyFootball.Application.UseCases.Leagues.Commands.UpdateLeague
{
    public class UpdateLeagueCommandValidator : AbstractValidator<UpdateLeagueCommand>
    {
        public UpdateLeagueCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("League Id is required.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("League name is required.");
        }
    }
}
