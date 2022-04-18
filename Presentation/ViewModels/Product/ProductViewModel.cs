namespace Presentation.ViewModels.Product
{
    public class ProductViewModel : ViewModelBase<long>
    {
        public string Sku { get; set; }

        public string Name { get; set; }

        public decimal UnitPrice { get; set; }

        public int StockQuantity { get; set; }
    }
}
