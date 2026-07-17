namespace FantasyFootball.Application.UseCases.Teams.Queries.GetTeamById
{
    public class GetTeamByIdQueryValidator : AbstractValidator<GetTeamByIdQuery>
    {
        public GetTeamByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Team Id is required.");
        }
    }
}
