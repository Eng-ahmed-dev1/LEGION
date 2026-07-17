namespace FantasyFootball.Application.UseCases.Leagues.Commands.RemoveMember
{
    public class RemoveMemberFromLeagueCommandValidator : AbstractValidator<RemoveMemberFromLeagueCommand>
    {
        public RemoveMemberFromLeagueCommandValidator()
        {
            RuleFor(x => x.LeagueId)
                .NotEmpty()
                .WithMessage("League Id is required.");

            RuleFor(x => x.ManagerId)
                .NotEmpty()
                .WithMessage("Manager Id is required.");
        }
    }
}
