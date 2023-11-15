using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class City:BaseEntity
{

    public string CityName { get; set; } = null!;

    public int? StateId { get; set; }

    public virtual ICollection<PostalCode> PostalCodes { get; set; } = new List<PostalCode>();

    public virtual State State { get; set; }
}
