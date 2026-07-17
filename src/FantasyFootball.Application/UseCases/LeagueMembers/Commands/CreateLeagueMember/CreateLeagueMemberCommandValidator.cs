namespace FantasyFootball.Application.UseCases.LeagueMembers.Commands.CreateLeagueMember
{
    public class CreateLeagueMemberCommandValidator : AbstractValidator<CreateLeagueMemberCommand>
    {
        public CreateLeagueMemberCommandValidator()
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
