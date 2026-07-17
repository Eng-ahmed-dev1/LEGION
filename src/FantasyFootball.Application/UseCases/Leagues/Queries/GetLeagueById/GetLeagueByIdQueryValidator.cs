namespace FantasyFootball.Application.UseCases.Leagues.Queries.GetLeagueById
{
    public class GetLeagueByIdQueryValidator : AbstractValidator<GetLeagueByIdQuery>
    {
        public GetLeagueByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("League Id is required.");
        }
    }
}
