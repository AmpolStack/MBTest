using AutoMapper;
using Shared.DTOs;
using Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WriterApi.Controllers;

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
    public async Task<IActionResult> GetAll()
    {
        var products = await _context.Products.ToListAsync();
        var mappers = _mapper.Map<List<ProductDto>>(products);
        return Ok(mappers);
    }
    
}