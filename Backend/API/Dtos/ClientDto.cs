using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace API.Dtos
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string name { get; set; } = null!;
        public int PersonId { get; set; }

        public string Phone { get; set; } = null!;

        public string Fax { get; set; } = null!;

        public decimal? CreditLimit { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    }
}