
namespace Application.Dto.Order
{
    public class OrderDto : DtoBase<Guid>
    {
        public Guid UserId { get; set; }

        public decimal Price { get; set; }

        public ICollection<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
    }
}
