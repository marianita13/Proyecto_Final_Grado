using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Employee: BaseEntity
{
    public int PersonId { get; set; }

    public string OfficeCode { get; set; } = null!;

    public int ManagerCode { get; set; }

    public string Position { get; set; }

    public virtual Office OfficeCodeNavigation { get; set; } = null!;

    public virtual Person Person { get; set; } = null!;
}
