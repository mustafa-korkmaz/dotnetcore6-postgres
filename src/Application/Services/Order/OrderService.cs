﻿
using Application.Dto.Order;
using AutoMapper;
using Domain.Aggregates.Product;
using Infrastructure.UnitOfWork;
using Infrastructure.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.Services.Order
{
    public class OrderService : ServiceBase<OrderRepository, Domain.Aggregates.Order.Order, OrderDto,Guid>, IOrderService
    {
        private readonly IProductRepository _productRepository;

        public OrderService(IUnitOfWork uow, ILogger<OrderService> logger, IMapper mapper)
        : base(uow, logger, mapper)
        {
            _productRepository = Uow.GetRepository<ProductRepository>();
        }

    }
}