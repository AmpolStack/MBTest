using AutoMapper;
using DbAccess.DTOs;
using DbAccess.Models;

namespace DbAccess.Custom;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
    }
}