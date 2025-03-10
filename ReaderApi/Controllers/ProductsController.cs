using System.Text;
using AutoMapper;
using Shared.DTOs;
using Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;

namespace ReaderApi.Controllers;

[ApiController]
[Route("Products")]
public class ProductsController : ControllerBase
{
    private readonly ProducersContext _context;
    private readonly IMapper _mapper;
    private readonly IConnection _connection;
    private readonly ILogger<ProductsController> _logger;
    public ProductsController(ProducersContext context, IMapper mapper, IConnection connection, ILoggerFactory factory)
    {
        _context = context;
        _mapper = mapper;
        _connection = connection;
        _logger = factory.CreateLogger<ProductsController>();
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

    [HttpGet("Send")]
    public async Task<IActionResult> Send([FromQuery] string message, 
        CancellationToken cancellationToken)
    {
        await using var channel = await _connection.CreateChannelAsync(cancellationToken: cancellationToken);
        var queue = await channel.QueueDeclareAsync("temp1", false, false, autoDelete: false,
            arguments: null, cancellationToken: cancellationToken);

        await channel.ExchangeDeclareAsync("principal", ExchangeType.Direct, cancellationToken: cancellationToken);
        var body = Encoding.UTF8.GetBytes(message);
        await channel.BasicPublishAsync(exchange: "principal", routingKey: "info", mandatory: true, body: body, cancellationToken);
        _logger.LogInformation($"SEND A MESSAGE {message}");
        return Ok(message);
    }
    
}