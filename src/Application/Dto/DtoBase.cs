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

    public class ListDtoResponse<TDto> where TDto : class
    {
        /// <summary>
        /// Paged list items
        /// </summary>
        public IReadOnlyCollection<TDto> Items { get; set; } = new List<TDto>();

        /// <summary>
        /// Total count of items stored in repository
        /// </summary>
        public long RecordsTotal { get; set; }
    }

    public class ListDtoRequest
    {
        public bool IncludeRecordsTotal { get; set; }

        public int Offset { get; set; }

        public int Limit { get; set; }
    }
}