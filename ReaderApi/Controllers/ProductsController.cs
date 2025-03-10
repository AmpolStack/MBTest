using AutoMapper;
using Shared.DTOs;
using Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ReaderApi.Controllers;

[ApiController]
[Route("Products")]
public class ProductsController : ControllerBase
{
    private readonly ProducersContext _context;
    private readonly IMapper _mapper;

    public ProductsController(ProducersContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult> GetProducts()
    {
        var products = await _context.Products.ToListAsync();
        var map = _mapper.Map<List<ProductDto>>(products);
        return Ok(map);
    }

    [HttpGet("GA2")]
    public async Task<ActionResult> GetGA2()
    {
        var response = await _context.Orders.Include(x => x.ProductOrders).ThenInclude(x => x.Product).ToListAsync();
        var mapping = _mapper.Map<List<OrderDto>>(response);
        return Ok(mapping);
    }
    
}