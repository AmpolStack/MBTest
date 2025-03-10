using System;
using System.Collections.Generic;

namespace Shared.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public decimal? Price { get; set; }

    public int? Stock { get; set; }

    public decimal? SpecificDiscount { get; set; }

    public virtual ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();
}
