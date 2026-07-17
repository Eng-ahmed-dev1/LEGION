namespace FantasyFootball.Application.UseCases.GameweekScores.Queries.GetGameweekScoreById
{
    public class GetGameweekScoreByIdQueryValidator : AbstractValidator<GetGameweekScoreByIdQuery>
    {
        public GetGameweekScoreByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Gameweek Score Id is required.");
        }
    }
}
