using FantasyFootball.Domain.Common;

namespace FantasyFootball.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DbSet<T> _dbSet;
        private readonly AppDbContext _context;
        public Repository(AppDbContext db)
        {
            _context = db;
            _dbSet = _context.Set<T>();
        }
        public async Task AddAsync(T entity)
        => await _dbSet.AddAsync(entity);

        public void Delete(T entity)
        => _dbSet.Remove(entity);

        public async Task<IReadOnlyList<T>> GetAllAsync()
        => await _dbSet.AsNoTracking().ToListAsync();

        public async Task<T?> GetByIdAsync(Guid id)
        => await _dbSet.FindAsync(id);

        public void Update(T entity)
        => _dbSet.Update(entity);
    }
}