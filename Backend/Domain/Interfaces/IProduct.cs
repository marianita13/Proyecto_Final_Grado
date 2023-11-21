using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
        
namespace Domain.Interfaces
{
    public interface IProduct:IGenericRepositoryS<Product>
    {
        Task<object> GetExpensiveProduct();
        Task<object> GetProductHigherStock();
        Task<object> GetProductLowerStock();
        Task<IEnumerable<object>> AnyProduct();
        List<object> GetProductsSalesOver3000Euros();
        List<string> GetProductWithMostUnitsSold();
        Task<IEnumerable<Product>> GetOrnamentalProductsOver100Stock();
        List<object> GetProductRangesPerClient();
    }
}
