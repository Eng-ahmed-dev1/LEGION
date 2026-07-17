namespace FantasyFootball.Application.UseCases.LeagueMembers.Queries.GetLeagueMemberById
{
    public class GetLeagueMemberByIdValidator : AbstractValidator<GetLeagueMemberByIdQuery>
    {
        public GetLeagueMemberByIdValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("League Member Id is required.");
        }
    }
}
