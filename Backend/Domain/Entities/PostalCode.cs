using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class PostalCode:BaseEntity
{


    public string PostalCode1 { get; set; } = null!;

    public int CityId { get; set; }

    public virtual City City { get; set; }

    public virtual ICollection<Office> Offices { get; set; } = new List<Office>();

    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
