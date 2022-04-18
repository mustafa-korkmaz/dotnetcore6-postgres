using Presentation.Middlewares.Validations;
using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels.Order
{
    public class AddEditOrderViewModel
    {
        [GuidValidation]
        public string? UserId { get; set; }

        [Required]
        public ICollection<AddEditOrderItemViewModel> Items { get; set; }
    }

    public class AddEditOrderItemViewModel
    {
        [LongValidation]
        public long ProductId { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
