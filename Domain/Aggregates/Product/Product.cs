namespace Domain.Aggregates.Product
{
    public class Product : LongEntity
    {
        public string Sku { get; private set; }

        public string Name { get; private set; }

        public decimal UnitPrice { get; private set; }

        public int StockQuantity { get; private set; }

        public Product(long id, string sku, string name, decimal unitPrice, int stockQuantity) : base(id)
        {
            Sku = sku;
            Name = name;
            UnitPrice = unitPrice;
            StockQuantity = stockQuantity;
        }

        public void RemoveStock(int quantity)
        {
            StockQuantity -= quantity;
        }
    }
}