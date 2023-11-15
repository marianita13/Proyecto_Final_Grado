<<<<<<< HEAD
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Client : BaseEntity
    {
        public string Name { get; set; }
    }
=======
﻿using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Client
{
    public int ClientCode { get; set; }

    public int PersonId { get; set; }

    public string Phone { get; set; } = null!;

    public string Fax { get; set; } = null!;

    public decimal? CreditLimit { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Person Person { get; set; } = null!;
>>>>>>> origin/Dev_duban
}
