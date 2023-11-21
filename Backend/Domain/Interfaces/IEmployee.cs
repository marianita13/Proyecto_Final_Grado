using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
        
namespace Domain.Interfaces
{
    public interface IEmployee:IGenericRepository<Employee>
    {
        Task<IEnumerable<object>> GetEmployeesByBossCode(int bossCode);
        Task<IEnumerable<object>> GetNonSalesRepresentatives();
        Task<IEnumerable<object>> GetCEOInformation();
        List<object> GetEmployeeHierarchy();
        List<object> GetEmployeesWithManagers();
    }
}
