using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class OrderDetail : BaseEntity
{
    public string ProductCode { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public short LineNumber { get; set; }
    public int OrderId { get; set; }

    public virtual Order Order { get; set; }

    public virtual Product Product { get; set; }
}
