namespace FantasyFootball.Application.UseCases.Managers.Commands.UpdateManagerRank
{
    public class UpdateManagerRankCommandValidator : AbstractValidator<UpdateManagerRankCommand>
    {
        public UpdateManagerRankCommandValidator()
        {
            RuleFor(x => x.ManagerId)
                .NotEmpty()
                .WithMessage("Manager Id is required.");

            RuleFor(x => x.NewRank)
                .GreaterThan(0)
                .WithMessage("Rank must be greater than 0.");
        }
    }
}
