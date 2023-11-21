using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
        
namespace Domain.Interfaces
{
    public interface IClient:IGenericRepository<Client>
    {
        Task<IEnumerable<Client>> GetClientNoPayment();
        Task<IEnumerable<Client>> GetClientNoOrder();
        Task<IEnumerable<Client>> GetClientNoPaymentNoOrder();
        Task<IEnumerable<Client>> GetClientNoPaymentYesOrder();
        
    }
}
