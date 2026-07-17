namespace FantasyFootball.Application.UseCases.Players.Commands.UpdatePlayer
{
    public class UpdatePlayerCommandValidator : AbstractValidator<UpdatePlayerCommand>
    {
        public UpdatePlayerCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Player Id is required.");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("First Name is required.");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Last Name is required.");

            RuleFor(x => x.Price)
                .InclusiveBetween(4.0m, 15.0m)
                .WithMessage("Price must be between 4.0 and 15.0.");
        }
    }
}
