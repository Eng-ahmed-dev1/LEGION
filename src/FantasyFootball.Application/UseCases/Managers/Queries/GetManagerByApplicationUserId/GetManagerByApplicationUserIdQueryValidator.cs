namespace FantasyFootball.Application.UseCases.Managers.Queries.GetManagerByApplicationUserId
{
    public class GetManagerByApplicationUserIdQueryValidator : AbstractValidator<GetManagerByApplicationUserIdQuery>
    {
        public GetManagerByApplicationUserIdQueryValidator()
        {
            RuleFor(x => x.ApplicationUserId)
                .NotEmpty()
                .WithMessage("Application User Id is required.");
        }
    }
}
