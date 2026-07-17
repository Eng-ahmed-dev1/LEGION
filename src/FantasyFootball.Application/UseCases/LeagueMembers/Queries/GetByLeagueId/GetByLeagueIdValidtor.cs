namespace FantasyFootball.Application.UseCases.LeagueMembers.Queries.GetByLeagueId
{
    public class GetByLeagueIdValidtor : AbstractValidator<GetByLeagueIdQuery>
    {
        public GetByLeagueIdValidtor()
        {
            RuleFor(x => x.LeagueId)
                .NotEmpty()
                .WithMessage("League Id is required");
        }
    }
}
