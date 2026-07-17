namespace FantasyFootball.Application.UseCases.Gameweeks.Commands.DeleteGameweek
{
    public class DeleteGameweekCommandValidator : AbstractValidator<DeleteGameweekCommand>
    {
        public DeleteGameweekCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Gameweek Id is required.");
        }
    }
}
