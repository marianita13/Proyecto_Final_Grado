using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Application.Repository
{
    public class SummaryRepository : GenericRepository<Summary>, ISummary
    {
        private readonly GardeningContext _context;


        public SummaryRepository(GardeningContext context) : base(context)
        {
            _context = context;
        }
        // 1. ¿Cuántos empleados hay en la compañía?
        public async Task<int> GetQuantiyEmployees()
        {
            return await _context.Employees.CountAsync();
        }

        // 2. ¿Cuántos clientes tiene cada país?
        public async Task<IEnumerable<object>> ClientQuantityForCountry()
        {
            return await (from country in _context.Countries
                            join state in _context.States on country.Id equals state.CountryId
                            join city in _context.Cities on state.Id equals city.StateId
                            join postalCode in _context.Postalcodes on city.Id equals postalCode.CityId
                            join client in _context.Clients on postalCode.Id equals client.PostalCodeId
                            group client by country.CountryName into groupedClients
                            select new
                            {
                                Country = groupedClients.Key,
                                NumberOfClients = groupedClients.Count()
                            }).ToListAsync();
        }
    }
}