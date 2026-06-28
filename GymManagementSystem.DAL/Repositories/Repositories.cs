using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.DAL.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        int SaveChanges();
    }

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly GymDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(GymDbContext context)
        {
            _context = context;
            _dbSet   = context.Set<T>();
        }

        public IEnumerable<T> GetAll()  => _dbSet.ToList();
        public T? GetById(int id)       => _dbSet.Find(id);
        public void Add(T entity)       => _dbSet.Add(entity);
        public void Update(T entity)    => _dbSet.Update(entity);
        public void Delete(T entity)    => _dbSet.Remove(entity);
        public int SaveChanges()        => _context.SaveChanges();
    }
}
