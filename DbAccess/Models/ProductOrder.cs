using System;
using System.Collections.Generic;

namespace DbAccess.Models;

public partial class ProductOrder
{
    public int ProductId { get; set; }

    public int OrderId { get; set; }

    public int? Amount { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
