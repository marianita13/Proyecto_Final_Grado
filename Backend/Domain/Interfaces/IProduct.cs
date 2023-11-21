using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
        
namespace Domain.Interfaces
{
    public interface IProduct:IGenericRepositoryS<Product>
    {
        Task<IEnumerable<Product>> GetProductsNoOrder();
        Task<IEnumerable<object>> GetProductsNoOrderWithInfo();
    }
}
