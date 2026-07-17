namespace FantasyFootball.Application.Common.Interfaces
{
    public interface ITransferRepository : IRepository<Transfer>
    {
        Task<IReadOnlyList<Transfer>> GetByFantasyTeamIdAsync(Guid fantasyTeamId);
        Task<IReadOnlyList<Transfer>> GetByGameweekIdAsync(Guid gameweekId);
    }
}
