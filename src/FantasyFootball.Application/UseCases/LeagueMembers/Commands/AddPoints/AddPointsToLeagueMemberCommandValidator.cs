namespace FantasyFootball.Application.UseCases.LeagueMembers.Commands.AddPoints
{
    public class AddPointsToLeagueMemberCommandValidator : AbstractValidator<AddPointsToLeagueMemberCommand>
    {
        public AddPointsToLeagueMemberCommandValidator()
        {
            RuleFor(x => x.LeagueMemberId)
                .NotEmpty()
                .WithMessage("LeagueMember Id is required.");

            RuleFor(x => x.Points)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Points cannot be negative.");
        }
    }
}
