namespace FantasyFootball.Application.UseCases.Players.Commands.DeletePlayer
{
    public class DeletePlayerCommandValidator : AbstractValidator<DeletePlayerCommand>
    {
        public DeletePlayerCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Player Id is required.");
        }
    }
}
