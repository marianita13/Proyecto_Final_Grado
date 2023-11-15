using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace API.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; } = null!;

        public int ProductLine { get; set; }

        public string Dimensions { get; set; }

        public int IdSupplier { get; set; }

        public string Description { get; set; }

        public short StockQuantity { get; set; }

        public decimal SellingPrice { get; set; }

        public decimal SupplierPrice { get; set; }


        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();


    }
}