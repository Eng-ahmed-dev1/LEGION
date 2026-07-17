namespace FantasyFootball.Application.UseCases.Teams.Queries.GetTeamByName
{
    public class GetTeamByNameQueryValidator : AbstractValidator<GetTeamByNameQuery>
    {
        public GetTeamByNameQueryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Team name is required.");
        }
    }
}
