using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Country: BaseEntity
{
    public string CountryName { get; set; } = null!;

    public virtual ICollection<State> States { get; set; } = new List<State>();
}
