using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ISummary : IGenericRepository<Summary>
    {
        Task<IEnumerable<object>> GetQuantiyEmployees();
        Task<IEnumerable<object>> ClientQuantityForCountry();
    }
}