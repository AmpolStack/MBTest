using AutoMapper;
using Shared.DTOs;
using Shared.Models;

namespace Shared.Custom;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<ProductOrder, ProductOrderDto>().ForMember(dest => dest.Product,
            opt => opt.MapFrom(x => x.Product)).ReverseMap();
        CreateMap<Order, OrderDto>().ForMember(dest => dest.Products,
            opt => opt.MapFrom(x => x.ProductOrders)).ReverseMap();
    }
}