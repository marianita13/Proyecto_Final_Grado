using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public int ClientCode { get; set; }

        public int MethodId { get; set; }

        public string TransactionId { get; set; } = null!;

        public DateOnly PaymentDate { get; set; }

        public decimal Total { get; set; }



    }
}