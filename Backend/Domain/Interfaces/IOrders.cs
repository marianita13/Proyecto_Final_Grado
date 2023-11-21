using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
        
namespace Domain.Interfaces
{
    public interface IOrders:IGenericRepository<Order>
    {
        Task<IEnumerable<string>> GetOrderStatusList();
        Task<IEnumerable<Order>> GetDelayedOrders();
        Task<IEnumerable<Order>> GetOrdersDeliveredEarly();
        Task<IEnumerable<Order>> GetRejectedOrdersIn2009();
        Task<IEnumerable<Order>> GetOrdersDeliveredInJanuary();
    }
}
