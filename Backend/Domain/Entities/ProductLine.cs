using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ProductLine
{
    public int CodProductLine { get; set; }

    public string ProductLine1 { get; set; } = null!;

    public stringw DescriptionText { get; set; }

    public stringw DescriptionHtml { get; set; }

    public stringw Image { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
