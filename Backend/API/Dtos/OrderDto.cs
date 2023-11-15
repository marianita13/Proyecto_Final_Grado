using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace API.Dtos
{
    public class OrderDto
    {
           public DateOnly OrderDate { get; set; }

    public DateOnly ExpectedDate { get; set; }

    public DateOnly? DeliveryDate { get; set; }

    public int StatusCode { get; set; }

    public string Comments { get; set; }

    public int ClientCode { get; set; }

 

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

 
    }
}