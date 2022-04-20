
namespace Presentation.ViewModels.Order
{
    public class OrderViewModel
    {
        public string Id { get; set; }

        public decimal Price { get; set; } 

        public string UserId { get; set; }

        public IReadOnlyCollection<OrderItemViewModel> Items { get; set; }
    }

    public class OrderItemViewModel
    {
        public long ProductId { get; private set; }

        public decimal UnitPrice { get; private set; }

        public int Quantity { get; private set; }
    }
}