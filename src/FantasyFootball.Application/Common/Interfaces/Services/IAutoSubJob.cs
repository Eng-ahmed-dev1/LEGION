namespace FantasyFootball.Application.Common.Interfaces.Services
{
    public interface IAutoSubJob
    {
        Task ProcessAutoSubsAsync(Guid gameweekId);
    }
}
