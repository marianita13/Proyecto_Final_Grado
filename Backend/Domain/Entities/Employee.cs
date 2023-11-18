using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Employee: BaseEntity
{
    public int PersonId { get; set; }

    public string Extention { get; set; }

    public string OfficeCode { get; set; }

    public int? ManagerCode { get; set; }

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    public virtual Office OfficeCodeNavigation { get; set; }

    public virtual Person Person { get; set; }
}
