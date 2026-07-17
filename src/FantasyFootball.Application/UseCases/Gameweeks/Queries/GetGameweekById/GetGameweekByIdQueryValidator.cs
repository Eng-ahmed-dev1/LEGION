namespace FantasyFootball.Application.UseCases.Gameweeks.Queries.GetGameweekById
{
    public class GetGameweekByIdQueryValidator : AbstractValidator<GetGameweekByIdQuery>
    {
        public GetGameweekByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Gameweek Id is required.");
        }
    }
}
