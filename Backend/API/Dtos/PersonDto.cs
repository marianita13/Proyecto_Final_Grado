using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace API.Dtos
{
    public class PersonDto
    {
        public string FirstName { get; set; } = null!;

    public string LastName1 { get; set; } = null!;

    public string LastName2 { get; set; }

    public string Extension { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int PersonTypeId { get; set; }

    public int PostalCodeId { get; set; }

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

  
    }
}