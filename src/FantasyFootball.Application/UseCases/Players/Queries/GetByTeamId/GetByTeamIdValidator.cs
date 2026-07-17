namespace FantasyFootball.Application.UseCases.Players.Queries.GetByTeamId
{
    public class GetByTeamIdValidator : AbstractValidator<GetByTeamIdQuery>
    {
        public GetByTeamIdValidator()
        {
            RuleFor(x => x.TeamId)
                .NotEmpty()
                .WithMessage("Team Id is required.");
        }
    }
}
