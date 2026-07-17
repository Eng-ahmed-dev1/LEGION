namespace FantasyFootball.Application.UseCases.GameweekScores.Queries.GetGameweekScoresByManagerId
{
    public class GetGameweekScoresByManagerIdValidtor : AbstractValidator<GetGameweekScoresByManagerIdQuery>
    {
        public GetGameweekScoresByManagerIdValidtor()
        {

            RuleFor(x => x.ManagerId)
                .NotEmpty()
                .WithMessage("The Manager id was not found");
        }
    }
}
