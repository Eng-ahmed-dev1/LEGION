namespace FantasyFootball.Application.UseCases.Transfers.Queries.GetTransfersByFantasyTeamId
{
    public class GetTransfersByFantasyTeamIdQueryValidator : AbstractValidator<GetTransfersByFantasyTeamIdQuery>
    {
        public GetTransfersByFantasyTeamIdQueryValidator()
        {
            RuleFor(x => x.FantasyTeamId)
                .NotEmpty()
                .WithMessage("Fantasy Team Id is required.");
        }
    }
}
