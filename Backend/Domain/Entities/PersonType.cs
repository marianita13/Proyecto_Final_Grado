using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class PersonType
{
    public int TypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
