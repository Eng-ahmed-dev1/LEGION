namespace FantasyFootball.Application.UseCases.Fixtures.Queries.GetFixtureById
{
    public class GetFixtureByIdQueryValidator : AbstractValidator<GetFixtureByIdQuery>
    {
        public GetFixtureByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Fixture Id is required.");
        }
    }
}
