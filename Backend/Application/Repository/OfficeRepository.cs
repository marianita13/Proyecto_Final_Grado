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

        /*
        -- 10. Devuelve las oficinas donde no trabajan ninguno de los empleados que hayan sido los representantes de ventas de algún cliente que haya realizado la compra de algún producto de la gama Frutales.
        -- employees.Id as employees, clients.Id as clients, orders.Id as orders, productlines.ProductLine1,
        SELECT DISTINCT offices.* FROM offices
        LEFT join employees ON employees.OfficeCode = offices.Id
        LEFT join clients ON clients.CodEmployee = employees.Id
        LEFT join orders ON orders.ClientCode = clients.Id
        LEFT join orderdetails ON orderdetails.OrderId = orders.Id
        LEFT join products ON products.Id = orderdetails.ProductCode
        LEFT join productlines ON products.ProductLine = productlines.Id
        WHERE productlines.ProductLine1 NOT LIKE "Frutales"
        -- AND orders.ClientCode IS ;
        */
        public async Task<IEnumerable<Office>> GetOfficeNoSellFruits()
        {
            return await (from office in _context.Offices
                          join employee in _context.Employees on office.Id equals employee.OfficeCode into employeeGroup
                          from employee in employeeGroup.DefaultIfEmpty()
                          join client in _context.Clients on employee.Id equals client.CodEmployee into clientGroup
                          from client in clientGroup.DefaultIfEmpty()
                          join order in _context.Orders on client.Id equals order.ClientCode into orderGroup
                          from order in orderGroup.DefaultIfEmpty()
                          join orderDetail in _context.Orderdetails on order.Id equals orderDetail.OrderId into orderDetailGroup
                          from orderDetail in orderDetailGroup.DefaultIfEmpty()
                          join product in _context.Products on orderDetail.ProductCode equals product.Id into productGroup
                          from product in productGroup.DefaultIfEmpty()
                          join productLine in _context.Productlines on product.ProductLine equals productLine.Id into productLineGroup
                          from productLine in productLineGroup.DefaultIfEmpty()
                          where productLine.ProductLine1 != "Frutales"
                          select office).Distinct()
                        .ToListAsync();
        }
    }
}