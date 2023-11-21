using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
        
namespace Domain.Interfaces
{
    public interface IEmployee:IGenericRepository<Employee>
    {
        Task<IEnumerable<object>> AlbertoSoriaEmployees();
        // Task<IEnumerable<object>> Employees_Dont_Represent_Client();
    }
}
