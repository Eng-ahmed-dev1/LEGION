namespace FantasyFootball.Application.Common.Abstractions;

public interface IPlayerNewsRepository
{
    Task<IEnumerable<PlayerNews>> GetActiveNewsAsync();
    Task<IEnumerable<PlayerNews>> GetNewsByPlayerIdAsync(Guid playerId);
}
