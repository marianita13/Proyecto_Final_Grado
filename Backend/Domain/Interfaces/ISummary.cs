using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ISummary : IGenericRepository<Summary>
    {
        Task<int> GetQuantiyEmployees();
        Task<IEnumerable<object>> ClientQuantityForCountry();
    }
}