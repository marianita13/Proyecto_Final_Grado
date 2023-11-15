<<<<<<< HEAD
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; set; }
    }
=======
﻿using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class City
{
    public int CityId { get; set; }

    public string CityName { get; set; } = null!;

    public int? StateId { get; set; }

    public virtual ICollection<PostalCode> PostalCodes { get; set; } = new List<PostalCode>();

    public virtual State State { get; set; }
>>>>>>> origin/Dev_duban
}
