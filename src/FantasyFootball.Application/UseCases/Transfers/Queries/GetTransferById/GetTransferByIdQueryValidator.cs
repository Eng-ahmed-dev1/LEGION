namespace FantasyFootball.Application.UseCases.Transfers.Queries.GetTransferById
{
    public class GetTransferByIdQueryValidator : AbstractValidator<GetTransferByIdQuery>
    {
        public GetTransferByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Transfer Id is required.");
        }
    }
}
