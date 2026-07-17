namespace FantasyFootball.Application.UseCases.Leagues.Commands.CreateLeague
{
    public class CreateLeagueCommandValidator : AbstractValidator<CreateLeagueCommand>
    {
        public CreateLeagueCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("League name is required.");

            RuleFor(x => x.CreatedById)
                .NotEmpty()
                .WithMessage("Creator Manager Id is required.");
        }
    }
}
