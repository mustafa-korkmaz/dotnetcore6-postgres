﻿
using Domain.Aggregates.Identity;

namespace Domain.Aggregates.Order
{
    public class Order : EntityBase<Guid>
    {
        public Guid UserId { get; private set; }

        public User? User { get; private set; }

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

        public Order(Guid id, Guid userId) : base(id)
        {
            _items = new List<OrderItem>();
            UserId = userId;
        }

        public void AddItem(long productId, decimal unitPrice, int quantity)
        {
            _items.Add(new OrderItem(UserId, productId, unitPrice, quantity));
        }
    }
}
