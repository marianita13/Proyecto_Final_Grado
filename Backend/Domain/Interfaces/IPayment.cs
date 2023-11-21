using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
        
namespace Domain.Interfaces
{
    public interface IPayment:IGenericRepository<Payment>
    {
         Task<IEnumerable<Payment>> GetPaymentsIn2008ByPaypal();
         Task<IEnumerable<string>> GetDistinctPaymentMethods();
    }
}
