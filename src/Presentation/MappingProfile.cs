﻿using Application.Dto;
using Application.Dto.Identity;
using Application.Dto.Order;
using Application.Dto.Product;
using AutoMapper;
using Presentation.ViewModels;
using Presentation.ViewModels.Identity;
using Presentation.ViewModels.Order;
using Presentation.ViewModels.Product;

namespace Presentation
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddUserViewModel, UserDto>()
                .ForMember(dest => dest.Username, opt =>
                    opt.MapFrom(source => source.Email));

            CreateMap<UserDto, UserViewModel>()
                .ForMember(dest => dest.CreatedAt, opt =>
                    opt.MapFrom(source => source.CreatedAt.UtcDateTime));

            CreateMap<UserDto, TokenViewModel>()
                .ForMember(dest => dest.CreatedAt, opt =>
                    opt.MapFrom(source => source.CreatedAt.UtcDateTime));

            CreateMap<AddEditProductViewModel, ProductDto>();
            CreateMap<ProductDto, ProductViewModel>();

            CreateMap<ListViewModelRequest, ListDtoRequest>();
            CreateMap(typeof(ListDtoResponse<>), typeof(ListViewModelResponse<>));

            CreateMap<AddEditOrderViewModel, OrderDto>();
            CreateMap<AddEditOrderItemViewModel, OrderItemDto>();
            CreateMap<OrderDto, OrderViewModel>();
            CreateMap<OrderItemDto, OrderItemViewModel>();
        }
    }
}