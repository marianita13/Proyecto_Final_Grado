using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Supplier: BaseEntity
{
    public string Name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Fax { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
