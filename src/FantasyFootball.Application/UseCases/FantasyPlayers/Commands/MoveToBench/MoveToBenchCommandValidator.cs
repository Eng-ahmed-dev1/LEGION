namespace FantasyFootball.Application.UseCases.FantasyPlayers.Commands.MoveToBench
{
    public class MoveToBenchCommandValidator : AbstractValidator<MoveToBenchCommand>
    {
        public MoveToBenchCommandValidator()
        {
            RuleFor(x => x.PlayerId)
                .NotEmpty()
                .WithMessage("PlayerId is required.");
        }
    }
}
