namespace FantasyFootball.Application.UseCases.FantasyTeams.Queries.GetFantasyTeamById
{
    public class GetFantasyTeamByIdQueryValidator : AbstractValidator<GetFantasyTeamByIdQuery>
    {
        public GetFantasyTeamByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Fantasy Team Id is required.");
        }
    }
}
