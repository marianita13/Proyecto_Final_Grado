using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
        
namespace Domain.Interfaces
{
    public interface IClient:IGenericRepository<Client>
    {
        Task<IEnumerable<string>> GetSpanishClients();
        IEnumerable<int> GetUniqueClientCodesWithPaymentsIn2008();
        List<object> GetClientsFromMadridWithSpecificEmployees();
        List<object> GetClientAndSalesRepresentativeInfo();
        List<string> GetClientsWithPaymentsAndSalesRepresentatives();
        List<Client> GetClientsWithoutPayments();
        List<string> GetClientsWithLateDeliveries();
        List<object> GetClientsWithoutPaymentsAndRepresentativesWithCity();
        List<object> GetClientsWithPaymentsAndRepresentativesWithCity();
        List<object> GetClientsWithoutPaymentsAndRepresentatives();
        List<object> GetClientsWithPaymentsAndRepresentatives();
        List<object> GetClientsWithRepresentativesAndOfficeCity();

    }
}
