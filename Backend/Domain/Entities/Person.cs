<<<<<<< HEAD
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Person : BaseEntity
    {
        public string Name { get; set; }
    }
=======
﻿using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Person
{
    public int PersonId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName1 { get; set; } = null!;

    public string LastName2 { get; set; }

    public string Extension { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int PersonTypeId { get; set; }

    public int PostalCodeId { get; set; }

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual PersonType PersonType { get; set; } = null!;

    public virtual PostalCode PostalCode { get; set; }
>>>>>>> origin/Dev_duban
}
