using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Supplier: BaseEntity
{
    public string Name { get; set; }

    public string Phone { get; set; }

    public string Fax { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
