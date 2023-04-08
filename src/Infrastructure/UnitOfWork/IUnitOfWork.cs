using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// The task result contains the number of state entries written to the database.
        /// </summary>
        /// <returns></returns>
        Task<int> SaveAsync();

        TRepository GetRepository<TRepository>() where TRepository : class;

        IDbContextTransaction BeginTransaction();
    }
}