namespace FantasyFootball.Application.UseCases.PlayerEvents.Queries.GetPlayerEventsByPlayerId
{
    public class GetPlayerEventsByPlayerIdQueryValidator : AbstractValidator<GetPlayerEventsByPlayerIdQuery>
    {
        public GetPlayerEventsByPlayerIdQueryValidator()
        {
            RuleFor(x => x.PlayerId)
                .NotEmpty()
                .WithMessage("Player Id is required.");
        }
    }
}
