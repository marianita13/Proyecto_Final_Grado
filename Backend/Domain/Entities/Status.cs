<<<<<<< HEAD
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Status : BaseEntity
    {
        public string Name { get; set; }
    }
=======
﻿using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Status
{
    public int CodStatus { get; set; }

    public string NameStatus { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
>>>>>>> origin/Dev_duban
}
