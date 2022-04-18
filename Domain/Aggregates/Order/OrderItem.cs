
namespace Domain.Aggregates.Order
{
    /// <summary>
    /// value type object should be immutable and should not have an identity
    /// </summary>
    public class OrderItem : EntityBase<Guid>
    {
        public Guid OrderId { get; private set; }

        public Order? Order { get; private set; }

        public long ProductId { get; private set; }

        public Product.Product? Product { get; private set; }

        public decimal UnitPrice { get; private set; }

        public int Quantity { get; private set; }

        internal OrderItem(Guid orderId, long productId, decimal unitPrice, int quantity)
        {
            OrderId = orderId;
            ProductId = productId;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }

        public decimal GetPrice()
        {
            return Quantity * UnitPrice;
        }
    }
}