namespace FantasyFootball.Application.Common.Interfaces
{
    public interface IFixtureRepository : IRepository<Fixture>
    {
        Task<IReadOnlyList<Fixture>> GetByGameweekIdAsync(Guid gameweekId);
    }
}
