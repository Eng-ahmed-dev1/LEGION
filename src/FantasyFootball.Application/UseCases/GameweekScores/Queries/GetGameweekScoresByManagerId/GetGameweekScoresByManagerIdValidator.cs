namespace FantasyFootball.Application.UseCases.GameweekScores.Queries.GetGameweekScoresByManagerId
{
    public class GetGameweekScoresByManagerIdValidator : AbstractValidator<GetGameweekScoresByManagerIdQuery>
    {
        public GetGameweekScoresByManagerIdValidator()
        {
            RuleFor(x => x.ManagerId)
                .NotEmpty()
                .WithMessage("Manager Id is required.");
        }
    }
}
