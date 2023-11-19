using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Product: BaseEntityS
{
    public string Name { get; set; }

    public int ProductLine { get; set; }

    public string Dimensions { get; set; }

    public int IdSupplier { get; set; }

    public string Description { get; set; }

    public short StockQuantity { get; set; }

    public decimal SellingPrice { get; set; }

    public decimal SupplierPrice { get; set; }

    public virtual Supplier Supplier { get; set; }

    public virtual ICollection<OrderDetail> Orderdetails { get; set; } = new List<OrderDetail>();

    public virtual ProductLine ProductLineNavigation { get; set; }
}
