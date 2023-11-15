﻿using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string Name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Fax { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}