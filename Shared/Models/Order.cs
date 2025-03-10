using System;
using System.Collections.Generic;

namespace Shared.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public decimal? Total { get; set; }

    public string? ClientName { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public DateTime? PaymentDate { get; set; }

    public virtual ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();
}
