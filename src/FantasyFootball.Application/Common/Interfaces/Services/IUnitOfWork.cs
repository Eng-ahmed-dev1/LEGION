namespace FantasyFootball.Application.Common.Interfaces.Services
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}