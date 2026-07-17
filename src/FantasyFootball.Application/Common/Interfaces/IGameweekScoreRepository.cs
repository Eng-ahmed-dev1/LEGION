namespace FantasyFootball.Application.Common.Interfaces
{
    public interface IGameweekScoreRepository : IRepository<GameweekScore>
    {
        Task<GameweekScore?> GetByManagerIdAsync(Guid managerId);
        Task<GameweekScore?> GetByManagerAndGameweekAsync(Guid managerId, Guid gameweekId);
    }
}
