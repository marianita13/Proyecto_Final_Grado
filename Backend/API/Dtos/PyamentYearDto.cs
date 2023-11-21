using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class PyamentYearDto
    {
        public DateOnly PaymentDate { get; set; }

        public decimal Total { get; set; }
    }
}