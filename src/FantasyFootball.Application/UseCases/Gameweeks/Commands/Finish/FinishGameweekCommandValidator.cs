namespace FantasyFootball.Application.UseCases.Gameweeks.Commands.Finish
{
    public class FinishGameweekCommandValidator : AbstractValidator<FinishGameweekCommand>
    {
        public FinishGameweekCommandValidator()
        {
            RuleFor(x => x.GameweekId)
                .NotEmpty()
                .WithMessage("Gameweek Id is required.");
        }
    }
}
