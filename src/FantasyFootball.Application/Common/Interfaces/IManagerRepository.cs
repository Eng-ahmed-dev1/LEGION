namespace FantasyFootball.Application.Common.Interfaces
{
    public interface IManagerRepository : IRepository<Manager>
    {
        Task<Manager?> GetByApplicationUserIdAsync(Guid applicationUserId);
    }
}
