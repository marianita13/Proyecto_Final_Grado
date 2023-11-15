using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Status
{
    public int CodStatus { get; set; }

    public string NameStatus { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
