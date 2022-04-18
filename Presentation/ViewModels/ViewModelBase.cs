using Presentation.Middlewares.Validations;
using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels
{
    public class ViewModelBase<TKey>
    {
        public TKey Id { get; set; } = default(TKey);
    }

    public class GuidIdViewModel
    {
        [GuidValidation]
        public string? Id { get; set; }

        public Guid Value => Id == null ? Guid.Empty : Guid.Parse(Id);
    }

    public class LongIdViewModel
    {
        [LongValidation]
        public long? Id { get; set; }

        public long Value => Id == null ? 0 : Id.Value;
    }

    public class ListViewModelRequest
    {
        public int Offset { get; set; }

        [Range(1, 1000)]
        public int Limit { get; set; }
    }

    /// <summary>
    /// for specific searches which requires extra filters
    /// </summary>
    /// <typeparam name="TSearchCriteria"></typeparam>
    public class ListViewModelRequest<TSearchCriteria> : ListViewModelRequest
    {
        public TSearchCriteria SearchCriteria { get; set; }
    }

    public class ListViewModelResponse<TViewModel> where TViewModel : class
    {
        /// <summary>
        /// Paged list items
        /// </summary>
        public IReadOnlyCollection<TViewModel> Items { get; set; }

        /// <summary>
        /// Total count of items stored in repository
        /// </summary>
        public long TotalCount { get; set; }
    }
}
