namespace FantasyFootball.Application.UseCases.Managers.Commands.DeleteManager
{
    public class DeleteManagerCommandValidator : AbstractValidator<DeleteManagerCommand>
    {
        public DeleteManagerCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Manager Id is required.");
        }
    }
}
