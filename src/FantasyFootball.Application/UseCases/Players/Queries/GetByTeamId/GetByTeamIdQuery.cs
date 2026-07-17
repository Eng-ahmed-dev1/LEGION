namespace FantasyFootball.Application.UseCases.Players.Queries.GetByTeamId
{
    public record GetByTeamIdQuery(Guid TeamId) : IRequest<Result<IReadOnlyList<PlayerDto>>>;
}
