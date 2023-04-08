using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection;
using Infrastructure.Persistance.Postgres;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PostgresDbContext _context;
        private bool _disposed;
        private readonly Dictionary<string, object> _repositories;

        public UnitOfWork(PostgresDbContext context)
        {
            _context = context;
            _repositories = new Dictionary<string, object>();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _disposed = true;
        }

        public TRepository GetRepository<TRepository>() where TRepository : class
        {
            var repositoryType = typeof(TRepository);
            var repositoryName = repositoryType.Name;

            if (_repositories.ContainsKey(repositoryType.Name))
                return (TRepository)_repositories[repositoryName];

            var repositoryInstance = Activator.CreateInstance(repositoryType, _context);

            if (repositoryInstance == null)
            {
                throw new ArgumentNullException(nameof(repositoryInstance));
            }

            _repositories.Add(repositoryName, repositoryInstance);

            return (TRepository)_repositories[repositoryName];
        }
    }
}