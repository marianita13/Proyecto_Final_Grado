using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace API.Dtos
{
    public class StatusDto
    {
        public int Id { get; set; }
        public string NameStatus { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}