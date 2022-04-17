using Domain.Aggregates;
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

        /// <summary>
        /// Returns entity repository inherited from  IRepository
        /// </summary>
        /// <typeparam name="TRepository"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        TRepository GetRepository<TRepository, TEntity>()
            where TEntity : class
            where TRepository : IRepository<TEntity>;

        /// <summary>
        /// Use only for repositories which does not have an entity class correlated to an actual db table
        /// </summary>
        /// <typeparam name="TRepository"></typeparam>
        /// <returns></returns>
        TRepository GetRepository<TRepository>();

        IDbContextTransaction BeginTransaction();
    }
}
