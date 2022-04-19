using Application.Dto;
using Domain.Aggregates;
using Infrastructure.UnitOfWork;
using Microsoft.Extensions.Logging;
using AutoMapper;
using System.Reflection;
using Application.Constants;
using Application.Exceptions;

namespace Application.Services
{
    /// <summary>
    /// Abstract class for basic create, update, delete and get operations.
    /// </summary>
    /// <typeparam name="TEntity">TEntity is db entity.</typeparam>
    /// <typeparam name="TDto">TDto is data transfer object.</typeparam>
    /// <typeparam name="TRepository"></typeparam>
    /// <typeparam name="TKey">TKey is PK type for entity</typeparam>
    public abstract class ServiceBase<TRepository, TEntity, TDto, TKey> : IService<TDto, TKey>
        where TEntity : class, IEntity<TKey>
        where TDto : DtoBase<TKey>
        where TRepository : IRepository<TEntity>
    {
        protected readonly IUnitOfWork Uow;
        protected readonly TRepository Repository;
        protected readonly IMapper Mapper;
        protected readonly ILogger Logger;

        protected ServiceBase(IUnitOfWork uow, ILogger logger, IMapper mapper)
        {
            Uow = uow;
            Repository = Uow.GetRepository<TRepository, TEntity>();
            Logger = logger;
            Mapper = mapper;
        }

        public virtual async Task<TDto?> GetByIdAsync(object id)
        {
            var entity = await Repository.GetByIdAsync(id);

            if (entity == null)
            {
                return null;
            }

            return Mapper.Map<TEntity, TDto>(entity);
        }


        public virtual async Task AddAsync(TDto dto)
        {
            var entity = Mapper.Map<TDto, TEntity>(dto);

            await Repository.AddAsync(entity);

            await Uow.SaveAsync();

            dto.Id = entity.Id;

        }

        public async Task AddRangeAsync(TDto[] dtoList)
        {
            var entities = Mapper.Map<TDto[], TEntity[]>(dtoList);

            await Repository.AddRangeAsync(entities);

            await Uow.SaveAsync();

            for (int i = 0; i < dtoList.Length; i++)
            {
                dtoList[i].Id = entities[i].Id;
            }
        }

        public virtual async Task UpdateAsync(TDto dto)
        {

            var entity = await Repository.GetByIdAsync(dto.Id);

            if (entity == null)
            {
                throw new ValidationException(ErrorMessages.RecordNotFound);
            }

            var type = typeof(TEntity);
            var entityProperties = type.GetProperties();

            foreach (PropertyInfo entityProperty in entityProperties)
            {
                //Get CreatedAt property value from entity.
                if (entityProperty.Name == "CreatedAt")
                {
                    PropertyInfo dtoProperty = typeof(TDto).GetProperty(entityProperty.Name); //POCO obj must have same prop as model

                    var value = entityProperty.GetValue(entity); //get value of entity

                    dtoProperty.SetValue(dto, value, null); //set dto.CreatedAt as entity.CreatedAt

                    break;
                }
            }

            entity = Mapper.Map<TDto, TEntity>(dto);

            Repository.Update(entity);

            await Uow.SaveAsync();
        }

        public virtual async Task DeleteByIdAsync(object id)
        {
            var entity = await Repository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new ValidationException(ErrorMessages.RecordNotFound);
            }

            var type = typeof(TEntity);

            var entityProperties = type.GetProperties();

            foreach (PropertyInfo entityProperty in entityProperties)
            {
                if (entityProperty.CanWrite && entityProperty.Name == "IsDeleted")
                {
                    //Entity has an 'IsDeleted' property. Perform soft deletion and return
                    await SoftDeleteAsync(entityProperty, entity);

                    return;
                }
            }

            Repository.Remove(entity);

            await Uow.SaveAsync();

            //log db record deletion as an info
            Logger.LogInformation($"'{type}' entity has been hard-deleted.");

        }

        private async Task SoftDeleteAsync(PropertyInfo propertyInfo, TEntity entity)
        {
            var type = typeof(TEntity);

            propertyInfo.SetValue(entity, true, null); //soft deletion

            Repository.Update(entity);

            await Uow.SaveAsync();

            //log db record modification as an info
            Logger.LogInformation($"'{type}' entity with ID: {entity.Id} has been soft deleted.");
        }
    }
}
