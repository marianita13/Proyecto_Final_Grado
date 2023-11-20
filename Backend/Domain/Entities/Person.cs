using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Person: BaseEntity
{

    public string FirstName { get; set; }

    public string LastName1 { get; set; }

    public string LastName2 { get; set; }

    public string Email { get; set; }

    public int PersonTypeId { get; set; }

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
 

    public virtual PersonType PersonType { get; set; }

}
