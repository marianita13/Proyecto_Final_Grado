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
    
    }
}
