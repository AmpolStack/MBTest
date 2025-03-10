namespace Shared.DTOs;

public class OrderDto
{
    public int OrderId { get; set; }
    public decimal? Total { get; set; }
    public string? ClientName { get; set; }
    public string PaymentMethod { get; set; } = null!;
    public DateTime? PaymentDate { get; set; }
    public IEnumerable<ProductOrderDto> Products { get; set; } = new List<ProductOrderDto>();
}
