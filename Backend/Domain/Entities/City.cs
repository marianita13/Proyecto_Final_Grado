using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class City: BaseEntity
{
public string CityName { get; set; }

    public int? StateId { get; set; }

    public virtual ICollection<PostalCode> Postalcodes { get; set; } = new List<PostalCode>();

    public virtual State State { get; set; }
}
