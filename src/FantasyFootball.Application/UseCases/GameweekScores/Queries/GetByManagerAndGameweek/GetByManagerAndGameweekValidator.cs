namespace FantasyFootball.Application.UseCases.GameweekScores.Queries.GetByManagerAndGameweek
{
    public class GetByManagerAndGameweekValidator : AbstractValidator<GetByManagerAndGameweekQuery>
    {
        public GetByManagerAndGameweekValidator()
        {
            RuleFor(x => x.ManagerId)
                .NotEmpty()
                .WithMessage("Manager Id is required.");

            RuleFor(x => x.GameweekId)
                .NotEmpty()
                .WithMessage("Gameweek Id is required.");
        }
    }
}
