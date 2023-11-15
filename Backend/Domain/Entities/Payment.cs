<<<<<<< HEAD
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Payment : BaseEntity
    {
        public string Name { get; set; }
    }
=======
﻿using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Payment
{
    public int ClientCode { get; set; }

    public int MethodId { get; set; }

    public string TransactionId { get; set; } = null!;

    public DateOnly PaymentDate { get; set; }

    public decimal Total { get; set; }

    public virtual Client ClientCodeNavigation { get; set; } = null!;

    public virtual MethodPayment Method { get; set; } = null!;
>>>>>>> origin/Dev_duban
}
