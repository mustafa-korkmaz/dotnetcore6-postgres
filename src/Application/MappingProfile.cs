using Application.Dto.Order;
using Application.Dto.Product;
using AutoMapper;
using Domain.Aggregates.Order;
using Domain.Aggregates.Product;

namespace Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap(typeof(ListEntityResponse<>), typeof(ListDtoResponse<>));

            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>()
              .ConvertUsing(src => new Product(src.Sku, src.Name, src.UnitPrice, src.StockQuantity));

            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>()
                .ConvertUsing((src, dest) =>
                {
                    var order = new Order(Guid.NewGuid(), src.UserId);

                    foreach (var item in src.Items)
                    {
                        order.AddItem(item.ProductId, item.UnitPrice, item.Quantity);
                    }

                    return order;
                });
        }
    }
}
