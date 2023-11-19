using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Payment :  BaseEntity 
{

    public int MethodId { get; set; }

    public string TransactionId { get; set; }
    public int ClientId { get; set; }

    public DateOnly PaymentDate { get; set; }

    public decimal Total { get; set; }

    public virtual Client Client { get; set; }

    public virtual MethodPayment Method { get; set; }
}
