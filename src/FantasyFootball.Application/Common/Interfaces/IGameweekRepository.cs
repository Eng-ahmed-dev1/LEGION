namespace FantasyFootball.Application.Common.Interfaces
{
    public interface IGameweekRepository : IRepository<Gameweek>
    {
        Task<Gameweek?> GetActiveGameweekAsync();
    }
}
