namespace FantasyFootball.Application.UseCases.LeagueMembers.Commands.UpdateLeagueMember
{
    public class UpdateLeagueMemberCommandValidator : AbstractValidator<UpdateLeagueMemberCommand>
    {
        public UpdateLeagueMemberCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("League Member Id is required.");

            RuleFor(x => x.LeagueId)
                .NotEmpty()
                .WithMessage("League Id is required.");

            RuleFor(x => x.ManagerId)
                .NotEmpty()
                .WithMessage("Manager Id is required.");

            RuleFor(x => x.TotalPoints)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Total points must be non-negative.");
        }
    }
}
