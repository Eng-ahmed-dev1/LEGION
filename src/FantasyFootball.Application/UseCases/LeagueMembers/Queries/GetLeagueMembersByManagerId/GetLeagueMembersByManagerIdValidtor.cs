namespace FantasyFootball.Application.UseCases.LeagueMembers.Queries.GetLeagueMembersByManagerId
{
    public class GetLeagueMembersByManagerIdValidtor : AbstractValidator<GetLeagueMembersByManagerIdQuery>
    {
        public GetLeagueMembersByManagerIdValidtor()
        {
            RuleFor(x => x.ManagerId)
                .NotEmpty()
                .WithMessage("Manager Id is required");
        }
    }
}
