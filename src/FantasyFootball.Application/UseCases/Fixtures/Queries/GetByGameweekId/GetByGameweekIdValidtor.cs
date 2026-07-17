namespace FantasyFootball.Application.UseCases.Fixtures.Queries.GetByGameweekId
{
    public class GetByGameweekIdValidtor : AbstractValidator<GetByGameweekIdQuery>
    {
        public GetByGameweekIdValidtor()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Gameweek Id required");
        }
    }
}
