namespace FantasyFootball.Application.Common.Interfaces
{
    public interface IPlayerEventRepository : IRepository<PlayerEvent>
    {
        Task<IReadOnlyList<PlayerEvent>> GetByPlayerIdAsync(Guid playerId);
        Task<IReadOnlyList<PlayerEvent>> GetByFixtureIdAsync(Guid fixtureId);
    }
}
