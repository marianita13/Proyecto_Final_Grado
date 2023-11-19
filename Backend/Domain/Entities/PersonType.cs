using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class PersonType: BaseEntity
{
    public string TypeName { get; set; }

    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
