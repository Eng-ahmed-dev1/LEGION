namespace FantasyFootball.Application.UseCases.PlayerEvents.Queries.GetPlayerEventById
{
    public class GetPlayerEventByIdQueryValidator : AbstractValidator<GetPlayerEventByIdQuery>
    {
        public GetPlayerEventByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Player Event Id is required.");
        }
    }
}
