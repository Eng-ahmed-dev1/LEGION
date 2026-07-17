namespace FantasyFootball.Application.Common.Interfaces
{
    public interface IPlayerRepository : IRepository<Player>
    {
        Task<IReadOnlyList<Player>> GetByTeamIdAsync(Guid teamId);
        Task<IReadOnlyList<Player>> GetByPositionAsync(PlayerPosition position);
        Task<IReadOnlyList<Player>> GetFilteredPlayersAsync(FantasyFootball.Application.DTOs.PlayerQueryParameters parameters);
    }
}
