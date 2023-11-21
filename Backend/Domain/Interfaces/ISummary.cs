using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ISummary : IGenericRepository<Summary>
    {
        Task<IEnumerable<object>> GetQuantiyEmployees();
        Task<IEnumerable<object>> PaymentAverage2009();
        Task<IEnumerable<object>> ClientQuantityForCountry();
        Task<IEnumerable<object>> OrderQuantityForStates();
        Task<IEnumerable<object>> ProductMoreExpensiveAndCheap();
        Task<IEnumerable<object>> GetQuantiyClients();
        Task<IEnumerable<object>> ClientInMadrid();
        Task<IEnumerable<object>> ClientsInCityWithM();
        Task<IEnumerable<object>> quantityClientsForEmployeeVents();
        Task<IEnumerable<object>> ClientNoEmployeeVents();
    }
}