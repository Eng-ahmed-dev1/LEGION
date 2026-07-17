namespace FantasyFootball.Application.UseCases.Leagues.Queries.GetLeaguesByManagerId
{
    public class GetLeaguesByManagerIdQueryValidator : AbstractValidator<GetLeaguesByManagerIdQuery>
    {
        public GetLeaguesByManagerIdQueryValidator()
        {
            RuleFor(x => x.ManagerId)
                .NotEmpty()
                .WithMessage("Manager Id is required.");
        }
    }
}
