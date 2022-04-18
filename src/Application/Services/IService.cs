using Application.Dto;

namespace Application.Services
{
    public interface IService<TDto, TKey>
        where TDto : IDto<TKey>
    {
        /// <summary>
        /// returns dto object by given id
        /// </summary>
        /// <param name="id"></param>
        Task<TDto?> GetByIdAsync(object id);

        /// <summary>
        /// creates new entity from given dto
        /// </summary>
        /// <param name="dto"></param>
        Task AddAsync(TDto dto);

        /// <summary>
        /// creates new entities as bulk insert from given dto list
        /// </summary>
        /// <param name="dtoList"></param>
        Task AddRangeAsync(TDto[] dtoList);

        /// <summary>
        /// updates given entity and returns affected row count.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>affected row count in db</returns>
        Task UpdateAsync(TDto dto);

        /// <summary>
        /// hard or soft deletes entity by given id
        /// </summary>
        /// <param name="id"></param>
        Task DeleteByIdAsync(object id);
    }
}
