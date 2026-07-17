namespace FantasyFootball.Application.UseCases.LeagueMembers.Queries.GetLeagueMembersByManagerId
{
    public class GetLeagueMembersByManagerIdValidator : AbstractValidator<GetLeagueMembersByManagerIdQuery>
    {
        public GetLeagueMembersByManagerIdValidator()
        {
            RuleFor(x => x.ManagerId)
                .NotEmpty()
                .WithMessage("Manager Id is required.");
        }
    }
}
