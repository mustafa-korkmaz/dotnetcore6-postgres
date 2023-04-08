﻿
using Application.Dto.Product;
using AutoMapper;
using Infrastructure.Repositories;
using Infrastructure.UnitOfWork;
using Microsoft.Extensions.Logging;

namespace Application.Services.Product
{
    public class ProductService : ServiceBase<ProductRepository, Domain.Aggregates.Product.Product, ProductDto, long>, IProductService
    {
        public ProductService(IUnitOfWork uow, ILogger<ProductService> logger, IMapper mapper)
        : base(uow, logger, mapper)
        {

        }
    }
}
