using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class OrderDetailDto
    {
        public int OrderCode { get; set; }

    public string ProductCode { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public short LineNumber { get; set; }


    }
}