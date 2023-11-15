using System;
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
}
