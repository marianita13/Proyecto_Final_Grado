using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Boss : BaseEntity
    {
        public int PersonId { get; set; }
        public string Extention { get; set; }
        public string OfficeCode { get; set; }
        public string Phone { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public Office Office { get; set; }
        public Person Person {get; set;}
    }
}