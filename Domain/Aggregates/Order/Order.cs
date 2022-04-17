
namespace Domain.Aggregates.Order
{
    public class Order : EntityBase<Guid>
    {
        public string Username { get; private set; }

        public decimal Price => Items.Sum(x => x.GetPrice());

        private ICollection<OrderItem> _items;
        public IReadOnlyCollection<OrderItem> Items
        {
            get
            {
                return _items.ToList();
            }
            private set
            {
                _items = value.ToList();
            }
        }

        public Order(Guid id, string username) : base(id)
        {
            _items = new List<OrderItem>();
            Username = username;
        }

        public void AddItem(string productId, decimal unitPrice, int quantity)
        {
            _items.Add(new OrderItem(productId, unitPrice, quantity));
        }
    }
}
