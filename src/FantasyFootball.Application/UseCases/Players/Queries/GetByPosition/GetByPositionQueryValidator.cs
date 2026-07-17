namespace FantasyFootball.Application.UseCases.Players.Queries.GetByPosition
{
    public class GetByPositionQueryValidator : AbstractValidator<GetByPositionQuery>
    {
        public GetByPositionQueryValidator()
        {
            RuleFor(x => x.Position)
                .NotEmpty()
                .WithMessage("Player position is required.")
                .IsInEnum()
                .WithMessage("Player position is invalid.");
        }
    }
}
