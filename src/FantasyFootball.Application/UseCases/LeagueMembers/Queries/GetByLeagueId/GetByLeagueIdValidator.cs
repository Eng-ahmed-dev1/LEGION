namespace FantasyFootball.Application.UseCases.LeagueMembers.Queries.GetByLeagueId
{
    public class GetByLeagueIdValidator : AbstractValidator<GetByLeagueIdQuery>
    {
        public GetByLeagueIdValidator()
        {
            RuleFor(x => x.LeagueId)
                .NotEmpty()
                .WithMessage("League Id is required.");
        }
    }
}
