using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Office: BaseEntityS
{

    public int PostalCodeId { get; set; }

    public string Phone { get; set; } = null!;

    public string AddressLine1 { get; set; } = null!;

    public string AddressLine2 { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual PostalCode PostalCode { get; set; } = null!;
}
