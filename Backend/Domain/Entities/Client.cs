using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Client: BaseEntity
{
    public string ClientName { get; set; }

    public int PersonId { get; set; }

    public string Phone { get; set; }

    public string Fax { get; set; }

    public string LineAddress { get; set; }

    public string LineAddress2 { get; set; }

    public int PostalCodeId { get; set; }

    public int? CodEmployee { get; set; }

    public decimal? CreditLimit { get; set; }

    public virtual Employee CodEmployeeNavigation { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Person Person { get; set; }
    
    public virtual PostalCode PostalCode { get; set; }
}
