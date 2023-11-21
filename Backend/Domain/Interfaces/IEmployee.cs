using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
        
namespace Domain.Interfaces
{
    public interface IEmployee:IGenericRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetEmployeeNoOffice();
        Task<IEnumerable<Employee>> GetEmployeeNoClient();
        Task<IEnumerable<object>> GetEmployeeNoClientWithOffice();
        Task<IEnumerable<Employee>> GetEmployeeNoOfficeNoClient();
        Task<IEnumerable<object>> GetEmployeeNoClientWithBoss();
    
        Task<IEnumerable<object>> GetEmployeesByBossCode(int bossCode);
        Task<IEnumerable<object>> GetNonSalesRepresentatives();
        Task<IEnumerable<object>> GetCEOInformation();
        List<object> GetEmployeeHierarchy();
        List<object> GetEmployeesWithManagers();
        Task<IEnumerable<object>> AlbertoSoriaEmployees();
        // Task<IEnumerable<object>> Employees_Dont_Represent_Client();
    }
}
