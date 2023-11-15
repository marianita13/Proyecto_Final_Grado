using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Order: BaseEntity
{

    public DateOnly OrderDate { get; set; }

    public DateOnly ExpectedDate { get; set; }

    public DateOnly? DeliveryDate { get; set; }

    public int StatusCode { get; set; }

    public string Comments { get; set; }

    public int ClientCode { get; set; }

    public virtual Client ClientCodeNavigation { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Status StatusCodeNavigation { get; set; } = null!;
}
