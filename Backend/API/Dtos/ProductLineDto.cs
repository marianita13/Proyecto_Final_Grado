using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace API.Dtos
{
    public class ProductLineDto
    {
        public int Id { get; set; }
        public string ProductLine1 { get; set; } = null!;

        public string DescriptionText { get; set; }

        public string DescriptionHtml { get; set; }

        public string Image { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}