using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace API.Dtos
{
    public class MethodPaymentDto
    {
        public int Id { get; set; }
        public string MethodPayment1 { get; set; }

        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}