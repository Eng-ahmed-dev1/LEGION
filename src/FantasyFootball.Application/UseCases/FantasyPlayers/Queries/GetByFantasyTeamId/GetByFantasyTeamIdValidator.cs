namespace FantasyFootball.Application.UseCases.FantasyPlayers.Queries.GetByFantasyTeamId
{
    public class GetByFantasyTeamIdValidator : AbstractValidator<GetByFantasyTeamIdQuery>
    {
        public GetByFantasyTeamIdValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Fantasy Team Id is required.");
        }
    }
}
