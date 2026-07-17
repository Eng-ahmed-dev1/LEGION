namespace FantasyFootball.Application.UseCases.Transfers.Queries.GetTransfersByGameweekId
{
    public class GetTransfersByGameweekIdQueryValidator : AbstractValidator<GetTransfersByGameweekIdQuery>
    {
        public GetTransfersByGameweekIdQueryValidator()
        {
            RuleFor(x => x.GameweekId)
                .NotEmpty()
                .WithMessage("Gameweek Id is required.");
        }
    }
}
