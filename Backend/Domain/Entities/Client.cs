using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Client: BaseEntity
{
    public int PersonId { get; set; }

    public string Phone { get; set; } = null!;

    public string Fax { get; set; } = null!;

    public decimal? CreditLimit { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Person Person { get; set; } = null!;
}
