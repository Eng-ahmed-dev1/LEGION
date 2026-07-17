namespace FantasyFootball.Application.UseCases.Players.Commands.CreatePlayer
{
    public class CreatePlayerCommandValidator : AbstractValidator<CreatePlayerCommand>
    {
        public CreatePlayerCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("First Name is required.");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Last Name is required.");

            RuleFor(x => x.TeamId)
                .NotEmpty()
                .WithMessage("Team Id is required.");

            RuleFor(x => x.Position)
                .NotEmpty()
                .WithMessage("Player position is required.")
                .IsInEnum()
                .WithMessage("Player position is invalid.");

            RuleFor(x => x.Price)
                .InclusiveBetween(4.0m, 15.0m)
                .WithMessage("Price must be between 4.0 and 15.0.");
        }
    }
}
