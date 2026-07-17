namespace FantasyFootball.Application.UseCases.FantasyPlayers.Queries.GetFantasyPlayerById
{
    public class GetFantasyPlayerByIdQueryValidator : AbstractValidator<GetFantasyPlayerByIdQuery>
    {
        public GetFantasyPlayerByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Fantasy Player Id is required.");
        }
    }
}
