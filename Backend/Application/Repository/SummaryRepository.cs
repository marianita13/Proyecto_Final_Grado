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
        public async Task<IEnumerable<object>> GetQuantiyEmployees()
        {
            return new[]
            {
                new {
                    quantityEmployees = await _context.Employees.CountAsync()
                }
            };
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

        // 3. ¿Cuál fue el pago medio en 2009?
        public async Task<IEnumerable<object>> PaymentAverage2009()
        {
            return new[]
            {
                new {
                    Average =await _context.Payments
                        .Where(p => p.PaymentDate.Year == 2009)
                        .AverageAsync(p => p.Total)
                    }
            };
        }
        public async Task<IEnumerable<object>> OrderQuantityForStates()
        {
            return await (from order in _context.Orders
                          join client in _context.Clients on order.ClientCode equals client.Id
                          join postalCode in _context.Postalcodes on client.PostalCodeId equals postalCode.Id
                          join city in _context.Cities on postalCode.CityId equals city.Id
                          join state in _context.States on city.StateId equals state.Id
                          group order by state.Id into groupedOrders
                          orderby groupedOrders.Count() descending
                          select new
                          {
                              State = groupedOrders.Key,
                              NumberOfOrders = groupedOrders.Count()
                          }).ToListAsync();
        }

        // 5. Calcula el precio de venta del producto más caro y más barato en una misma consulta.
        public async Task<IEnumerable<object>> ProductMoreExpensiveAndCheap()
        {
            var result = await (from product in _context.Products
                                select product.SellingPrice).ToListAsync();

            var minPrice = result.Min();
            var maxPrice = result.Max();

            return new[]
            {
                new { MinPrice = minPrice, MaxPrice = maxPrice }
            };
        }

        // 6. Calcula el número de clientes que tiene la empresa.
        public async Task<IEnumerable<object>> GetQuantiyClients()
        {
            return new[]
            {
                new {
                    quantityClients =await _context.Clients.CountAsync()
                }
            };
        }

        // 7. ¿Cuántos clientes existen con domicilio en la ciudad de Madrid?
        public async Task<IEnumerable<object>> ClientInMadrid()
        {
            return new[]
            {
                new {
                    NumOfClients = await (from client in _context.Clients
                          join postalCode in _context.Postalcodes on client.PostalCodeId equals postalCode.Id
                          join city in _context.Cities on postalCode.CityId equals city.Id
                          where city.CityName == "Madrid"
                          select client).CountAsync()
                    }
            };
        }

        // 8. ¿Calcula cuántos clientes tiene cada una de las ciudades que empiezan por M?
        public async Task<IEnumerable<object>> ClientsInCityWithM()
        {
            return await (from client in _context.Clients
                          join postalCode in _context.Postalcodes on client.PostalCodeId equals postalCode.Id
                          join city in _context.Cities on postalCode.CityId equals city.Id
                          where city.CityName.StartsWith("M")
                          group client by city.CityName into groupedClients
                          select new
                          {
                              CityName = groupedClients.Key,
                              NumberOfClients = groupedClients.Count()
                          }).ToListAsync();
        }

        // 9. Devuelve el nombre de los representantes de ventas y el número de clientes al que atiende cada uno.
        public async Task<IEnumerable<object>> quantityClientsForEmployeeVents()
        {
            return await (from employee in _context.Employees
                          join person in _context.Persons on employee.PersonId equals person.Id
                          join client in _context.Clients on employee.Id equals client.CodEmployee into clientGroup
                          where person.PersonTypeId == 5
                          group new { employee, person, clientGroup } by employee.PersonId into groupedResults
                          select new
                          {
                              VentsEmployee = groupedResults.First().person.FirstName,
                              NumberOfClients = groupedResults.Sum(x => x.clientGroup.Count())
                          }).ToListAsync();

        }

        // 10. Calcula el número de clientes que no tiene asignado representante de ventas.
        public async Task<IEnumerable<object>> ClientNoEmployeeVents()
        {
            return new[]
            {new { 
                quantityClientsForEmployeeVents = await (from client in _context.Clients
                                                    join employee in _context.Employees on client.CodEmployee equals employee.Id into employeeGroup
                                                    from subEmployee in employeeGroup.DefaultIfEmpty()
                                                    join person in _context.Persons on subEmployee.PersonId equals person.Id
                                                    where person.PersonTypeId != 5
                                                    select client).CountAsync()
                }
            };
        }




    }
}