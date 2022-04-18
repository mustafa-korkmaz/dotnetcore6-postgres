using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels.Product
{
    public class AddEditProductViewModel
    {
        [Required]
        public string Sku { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public decimal StockQuantity { get; set; }
    }
}
