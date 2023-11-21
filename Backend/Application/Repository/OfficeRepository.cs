using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<object>> GetCitiesWithOffices()
        {
            return await (from office in _context.Offices
                          join postalCode in _context.Postalcodes on office.PostalCodeId equals postalCode.Id
                          join city in _context.Cities on postalCode.CityId equals city.Id
                          select new { office.Id, city.CityName })
                .ToListAsync();
        }

        public async Task<IEnumerable<object>> GetCitiesWithOfficesInSpain()
        {
            return await (from office in _context.Offices
                          join postalCode in _context.Postalcodes on office.PostalCodeId equals postalCode.Id
                          join city in _context.Cities on postalCode.CityId equals city.Id
                          where city.State.Country.CountryName == "España"  // Ajusta según la propiedad real en tu entidad City
                          select new { office.Id, city.CityName, office.Phone })
                        .ToListAsync();
        }



    }
}