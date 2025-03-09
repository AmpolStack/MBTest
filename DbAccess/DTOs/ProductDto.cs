namespace DbAccess.DTOs;

public class ProductDto
{
        public int ProductId { get; set; }

        public string Name { get; set; } = null!;

        public decimal? Price { get; set; }

        public int? Stock { get; set; }

        public decimal? SpecificDiscount { get; set; }
}