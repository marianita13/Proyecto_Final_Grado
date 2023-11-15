<<<<<<< HEAD
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Supplier : BaseEntity
    {
        public string Name { get; set; }
    }
=======
﻿using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string Name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Fax { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
>>>>>>> origin/Dev_duban
}
