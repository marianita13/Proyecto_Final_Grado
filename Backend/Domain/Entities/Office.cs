using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Office: BaseEntityS
{
    public int PostalCodeId { get; set; }

    public string Phone { get; set; }

    public string AddressLine1 { get; set; }

    public string AddressLine2 { get; set; }

    public virtual PostalCode PostalCode { get; set; }
    public ICollection<Employee> Employees { get; set; }
}
