namespace FantasyFootball.Application.UseCases.Players.Queries.GetPlayerById
{
    public class GetPlayerByIdQueryValidator : AbstractValidator<GetPlayerByIdQuery>
    {
        public GetPlayerByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Player Id is required.");
        }
    }
}
