using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Status:BaseEntity
{


    public string NameStatus { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
