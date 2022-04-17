
using Application.Constants;
using Application.Dto.Order;
using AutoMapper;
using Domain.Aggregates.Order;
using Domain.Aggregates.Product;
using Infrastructure.UnitOfWork;
using Microsoft.Extensions.Logging;

namespace Application.Services.Order
{
    public class OrderService : ServiceBase<IOrderRepository, Domain.Aggregates.Order.Order, OrderDto,Guid>, IOrderService
    {
        private readonly IProductRepository _productRepository;

        public OrderService(IUnitOfWork uow, ILogger<OrderService> logger, IMapper mapper)
        : base(uow, logger, mapper)
        {
            _productRepository = Uow.GetRepository<IProductRepository, Domain.Aggregates.Product.Product>();
        }

    }
}