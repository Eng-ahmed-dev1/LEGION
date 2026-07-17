namespace FantasyFootball.Application.Common.Interfaces
{
    public interface ILeagueMemberRepository : IRepository<LeagueMember>
    {
        Task<IReadOnlyList<LeagueMember>> GetByLeagueIdAsync(Guid leagueId);
        Task<IReadOnlyList<LeagueMember>> GetByManagerIdAsync(Guid managerId);

    }
}
