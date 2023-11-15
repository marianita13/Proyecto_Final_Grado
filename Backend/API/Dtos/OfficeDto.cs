using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace API.Dtos
{
    public class OfficeDto
    {
        public int PostalCodeId { get; set; }

    public string Phone { get; set; } = null!;

    public string AddressLine1 { get; set; } = null!;

    public string AddressLine2 { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    }
}