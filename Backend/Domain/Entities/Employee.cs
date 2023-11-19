using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Employee: BaseEntity
{
    public int PersonId { get; set; }
    public string Extention { get; set; }
    public string OfficeCode { get; set; }
    public int BossCode { get; set; }

    public  ICollection<Client> Clients { get; set; } = new List<Client>();  
    public Boss Boss {get; set;}
    public Person Person { get; set; }
    public Office Office { get; set; }
}
