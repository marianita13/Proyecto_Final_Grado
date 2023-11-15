using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class MethodPayment: BaseEntity
{
    public string MethodPayment1 { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
