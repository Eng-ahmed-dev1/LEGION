namespace FantasyFootball.Application.UseCases.Leagues.Commands.AddMember
{
    public class AddMemberToLeagueCommandValidator : AbstractValidator<AddMemberToLeagueCommand>
    {
        public AddMemberToLeagueCommandValidator()
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
