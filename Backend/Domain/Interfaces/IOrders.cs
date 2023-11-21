using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
        
namespace Domain.Interfaces
{
    public interface IOrders:IGenericRepository<Order>
    {
        Task<IEnumerable<object>> GetOrderStatusList();
        Task<IEnumerable<object>> GetDelayedOrders();
        Task<IEnumerable<object>> GetOrdersDeliveredEarly();
        Task<IEnumerable<object>> GetRejectedOrdersIn2009();
        Task<IEnumerable<object>> GetOrdersDeliveredInJanuary();
    }
}
