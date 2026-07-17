namespace FantasyFootball.Application.UseCases.LeagueMembers.Queries.GetLeagueMemberById
{
    public class GetLeagueMemberByIdValidtor : AbstractValidator<GetLeagueMemberByIdQuery>
    {
        public GetLeagueMemberByIdValidtor()
        {
            RuleFor(x => x.Id)
                 .NotEmpty()
                 .WithMessage("The Id is required");
        }
    }
}
