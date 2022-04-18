
namespace Application.Dto
{
    public interface IDto<TKey>
    {
        TKey Id { get; set; }
    }

    public class DtoBase<TKey> : IDto<TKey>
    {
        public TKey Id { get; set; } = default(TKey);
    }

    public class ListDtoResponse<TDto, TKey> where TDto : IDto<TKey>
    {
        /// <summary>
        /// Paged list items
        /// </summary>
        public IReadOnlyCollection<TDto> Items { get; set; } = new List<TDto>();

        /// <summary>
        /// Total count of items stored in repository
        /// </summary>
        public long TotalCount { get; set; }
    }
}
