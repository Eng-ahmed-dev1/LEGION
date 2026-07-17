namespace FantasyFootball.Application.UseCases.Managers.Commands.UpdateManager
{
    public class UpdateManagerCommandValidator : AbstractValidator<UpdateManagerCommand>
    {
        public UpdateManagerCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Manager Id is required.");

            RuleFor(x => x.TeamName)
                .NotEmpty()
                .WithMessage("Team name is required.");

            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("Username is required.");
        }
    }
}
