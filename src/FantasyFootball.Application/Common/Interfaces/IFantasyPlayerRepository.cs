namespace FantasyFootball.Application.Common.Interfaces
{
    public interface IFantasyPlayerRepository : IRepository<FantasyPlayer>
    {

        Task<FantasyPlayer?> GetByFantasyTeamIdAsync(Guid fantasyTeamId);
    }
}
