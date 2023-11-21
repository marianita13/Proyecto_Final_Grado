using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
        
namespace Domain.Interfaces
{
    public interface IOffice:IGenericRepositoryS<Office>
    {
        Task<IEnumerable<object>> GetCitiesWithOffices();
        Task<IEnumerable<object>> GetCitiesWithOfficesInSpain();
        List<string> GetOfficesWithClientsInFuenlabrada();
    
}
}
