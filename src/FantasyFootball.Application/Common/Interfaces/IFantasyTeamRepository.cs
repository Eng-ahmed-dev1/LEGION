namespace FantasyFootball.Application.Common.Interfaces
{
    public interface IFantasyTeamRepository : IRepository<FantasyTeam>
    {
        Task<FantasyTeam?> GetByManagerIdAsync(Guid managerId);
        Task<FantasyTeam?> GetByIdWithPlayersAsync(Guid id);
    }
}
