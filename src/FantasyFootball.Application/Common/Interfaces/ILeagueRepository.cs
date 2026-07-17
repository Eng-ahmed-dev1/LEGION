namespace FantasyFootball.Application.Common.Interfaces
{
    public interface ILeagueRepository : IRepository<League>
    {
        Task<IReadOnlyList<League>> GetByManagerIdAsync(Guid managerId);
    }
}
