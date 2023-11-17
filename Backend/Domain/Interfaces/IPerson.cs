using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
        
namespace Domain.Interfaces
{
    public interface IPerson:IGenericRepository<Person>
    {
        
        Task<Person>GetByFirstNameAsync (string FirstName);
        Task<Person> GetByRefreshTokenAsync(string FirstName);
    
    }
}
