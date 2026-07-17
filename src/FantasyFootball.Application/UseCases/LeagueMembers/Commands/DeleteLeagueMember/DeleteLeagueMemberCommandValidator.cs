namespace FantasyFootball.Application.UseCases.LeagueMembers.Commands.DeleteLeagueMember
{
    public class DeleteLeagueMemberCommandValidator : AbstractValidator<DeleteLeagueMemberCommand>
    {
        public DeleteLeagueMemberCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("League Member Id is required.");
        }
    }
}
