namespace FantasyFootball.Infrastructure.Repositories
{
    public class PlayerRepository : Repository<Player>, IPlayerRepository
    {
        public PlayerRepository(AppDbContext context) : base(context) { }
        public async Task<IReadOnlyList<Player>> GetByPositionAsync(PlayerPosition position)
        => await _dbSet.AsNoTracking().Where(x => x.Position == position).ToListAsync();

        public async Task<IReadOnlyList<Player>> GetByTeamIdAsync(Guid id)
        => await _dbSet.AsNoTracking().Where(x => x.TeamId == id).ToListAsync();

        public async Task<IReadOnlyList<Player>> GetFilteredPlayersAsync(FantasyFootball.Application.DTOs.PlayerQueryParameters parameters)
        {
            var query = _dbSet.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(parameters.Search))
            {
                var searchTerm = parameters.Search;
                query = query.Where(x => x.FirstName.Contains(searchTerm) || x.LastName.Contains(searchTerm));
            }

            if (parameters.TeamId.HasValue)
            {
                query = query.Where(x => x.TeamId == parameters.TeamId.Value);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Position) && Enum.TryParse<PlayerPosition>(parameters.Position, true, out var pos))
            {
                query = query.Where(x => x.Position == pos);
            }

            // Note: Price is a ValueObject. Assuming it has a Value property of type decimal.
            // EF Core typically maps owned types directly or with a navigation property.
            // If Price is mapped as a single column (e.g. Price_Value), this will work:
            if (parameters.MinPrice.HasValue)
            {
                query = query.Where(x => x.Price.Value >= parameters.MinPrice.Value);
            }

            if (parameters.MaxPrice.HasValue)
            {
                query = query.Where(x => x.Price.Value <= parameters.MaxPrice.Value);
            }

            // Sorting
            if (!string.IsNullOrWhiteSpace(parameters.SortBy))
            {
                bool isDesc = parameters.SortDirection?.ToLower() == "desc";
                query = parameters.SortBy.ToLower() switch
                {
                    "price" => isDesc ? query.OrderByDescending(x => x.Price.Value) : query.OrderBy(x => x.Price.Value),
                    "name" => isDesc ? query.OrderByDescending(x => x.FirstName) : query.OrderBy(x => x.FirstName),
                    "points" => isDesc ? query.OrderByDescending(x => x.TotalPoints.Point) : query.OrderBy(x => x.TotalPoints.Point),
                    _ => isDesc ? query.OrderByDescending(x => x.Id) : query.OrderBy(x => x.Id)
                };
            }
            else
            {
                query = query.OrderBy(x => x.Id);
            }

            // Pagination
            var skip = (parameters.Page - 1) * parameters.PageSize;
            query = query.Skip(skip).Take(parameters.PageSize);

            return await query.ToListAsync();
        }
    }
}
