
namespace Application.Dto.Order
{
    public class OrderItemDto
    {
        public long ProductId { get; private set; }

        public decimal UnitPrice { get; private set; }

        public int Quantity { get; private set; }
    }
}
