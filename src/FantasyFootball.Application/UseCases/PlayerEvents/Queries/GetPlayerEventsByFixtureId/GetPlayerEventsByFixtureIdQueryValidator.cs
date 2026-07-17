namespace FantasyFootball.Application.UseCases.PlayerEvents.Queries.GetPlayerEventsByFixtureId
{
    public class GetPlayerEventsByFixtureIdQueryValidator : AbstractValidator<GetPlayerEventsByFixtureIdQuery>
    {
        public GetPlayerEventsByFixtureIdQueryValidator()
        {
            RuleFor(x => x.FixtureId)
                .NotEmpty()
                .WithMessage("Fixture Id is required.");
        }
    }
}
