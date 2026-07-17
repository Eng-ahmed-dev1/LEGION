namespace FantasyFootball.Application.UseCases.Managers.Queries.GetManagerById
{
    public class GetManagerByIdQueryValidator : AbstractValidator<GetManagerByIdQuery>
    {
        public GetManagerByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Manager Id is required.");
        }
    }
}
