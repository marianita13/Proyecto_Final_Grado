using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
        
namespace Domain.Interfaces
{
    public interface IClient:IGenericRepository<Client>
    {
        Task<IEnumerable<Client>> GetClientNoPayment();
        Task<IEnumerable<Client>> GetClientNoOrder();
        Task<IEnumerable<Client>> GetClientNoPaymentNoOrder();
        Task<IEnumerable<Client>> GetClientNoPaymentYesOrder();
        
        Task<IEnumerable<string>> GetSpanishClients();
        IEnumerable<object> GetUniqueClientCodesWithPaymentsIn2008();
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

        Task<object> GetBigCreditLimit();
        Task<IEnumerable<object>> GetCreditAndPayment();
        Task<IEnumerable<object>> Clients_With_Any_Audio();
    }
}
