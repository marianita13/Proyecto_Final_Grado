using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;

namespace Application.Repository
{
    public class OfficeRepository : GenericRepositoryS<Office>, IOffice
    {
        private readonly GardeningContext _context;
        

        public OfficeRepository(GardeningContext context) : base(context)
        {
            _context = context;
        }

        /* Devuelve un listado con el código de oficina y la ciudad donde hay oficinas. */
        public async Task<IEnumerable<object>> GetCitiesWithOffices()
        {
            return await (from office in _context.Offices
                join postalCode in _context.Postalcodes on office.PostalCodeId equals postalCode.Id
                join city in _context.Cities on postalCode.CityId equals city.Id
                select new { office.Id, city.CityName })
                .ToListAsync();
        }


        /* Devuelve un listado con la ciudad y el telé fono de las oficinas de España. */
    public async Task<IEnumerable<object>> GetCitiesWithOfficesInSpain()
    {
    return await (from office in _context.Offices
                join postalCode in _context.Postalcodes on office.PostalCodeId equals postalCode.Id
                join city in _context.Cities on postalCode.CityId equals city.Id
                join state in _context.States on city.StateId equals state.Id
                where state.Country.CountryName == "España" 
                select new { office.Id, city.CityName, office.Phone })
                .ToListAsync();
    }

/* Lista la dirección de las oficinas que tengan clientes en Fuenlabrada. */
public List<string> GetOfficesWithClientsInFuenlabrada()
{
    var officesInFuenlabrada = _context.Offices
        .Where(office => office.Employees
                            .Any(employee => employee.Clients
                                .Any(client => client.PostalCode.City.CityName == "Fuenlabrada")))
        .Select(office => $"{office.AddressLine1}, {office.AddressLine2}")
        .ToList();

    return officesInFuenlabrada;
}








        
    }
}