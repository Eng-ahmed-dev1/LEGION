namespace FantasyFootball.Application.UseCases.FantasyTeams.Queries.GetFantasyTeamByManagerId
{
    public class GetFantasyTeamByManagerIdValidator : AbstractValidator<GetFantasyTeamByManagerIdQuery>
    {
        public GetFantasyTeamByManagerIdValidator()
        {
            RuleFor(x => x.ManagerId)
                .NotEmpty()
                .WithMessage("Manager Id is required.");
        }
    }
}
