
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection;
using Infrastructure.Persistance.Postgres;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PostgresDbContext _context;
        private bool _disposed;
        private Dictionary<string, object>? _repositories;

        public UnitOfWork(PostgresDbContext context)
        {
            _context = context;
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

        public TRepository GetRepository<TRepository>()
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, object>();
            }

            var type = typeof(TRepository).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryInterfaceType = typeof(TRepository);

                var assignedTypesToRepositoryInterface = Assembly.GetExecutingAssembly().GetTypes().Where(t => repositoryInterfaceType.IsAssignableFrom(t)); //all types of your plugin

                var repositoryType = assignedTypesToRepositoryInterface.First(p => p.Name[0] != 'I'); //filter interfaces, select only first implemented class

                var repositoryInstance = Activator.CreateInstance(repositoryType, _context);

                if (repositoryInstance == null)
                {
                    throw new ArgumentNullException(nameof(repositoryInstance));
                }

                _repositories.Add(type, repositoryInstance);
            }

            return (TRepository)_repositories[type];
        }

    }
}
