using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace API.Dtos
{
    public class BossDto
    {
        public int Id {get; set;}
        public int PersonId { get; set; }
        public string Extention { get; set; }
        public string OfficeCode { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}