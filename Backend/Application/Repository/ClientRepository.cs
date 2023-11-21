using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;

namespace Application.Repository
{
    public class ClientRepository : GenericRepository<Client>, IClient
    {
        private readonly GardeningContext _context;

        public ClientRepository(GardeningContext context) : base(context)
        {
            _context = context;
        }

/* Devuelve un listado con el nombre de los todos los clientes españoles */
        public async Task<IEnumerable<string>> GetSpanishClients()
{
    try
    {
        var spanishClients = await (
            from client in _context.Clients
            join person in _context.Persons on client.PersonId equals person.Id
            join postalCode in _context.Postalcodes on client.PostalCodeId equals postalCode.Id
            join city in _context.Cities on postalCode.CityId equals city.Id
            join country in _context.Countries on city.State.CountryId equals country.Id
            where country.CountryName == "España"
            select person.FirstName + " " + person.LastName1
        ).ToListAsync();

        return spanishClients;
    }
    catch (Exception ex)
    {

        Console.WriteLine($"Error al obtener clientes españoles: {ex.Message}");
        throw; 
    }
}

/*  Devuelve un listado con el código de cliente de aquellos clientes que
realizaron algún pago en 2008. Tenga en cuenta que deberá eliminar
aquellos códigos de cliente que aparezcan repetidos. Resuelva la consulta */
public IEnumerable<int> GetUniqueClientCodesWithPaymentsIn2008()
{
    var clientCodesWithPaymentsIn2008 = _context.Payments
        .Where(payment => payment.PaymentDate.Year == 2008)
        .Select(payment => payment.IdNavigation.PersonId) 
        .Distinct()
        .OrderBy(clientCode => clientCode); 

    return clientCodesWithPaymentsIn2008;
}

/*         Devuelve un listado con todos los clientes que sean de la ciudad de Madrid y
cuyo representante de ventas tenga el código de empleado 11 o 30. */
public List<object> GetClientsFromMadridWithSpecificEmployees()
{
    var clients = from client in _context.Clients
                join employee in _context.Employees on client.CodEmployee equals employee.PersonId
                join person in _context.Persons on client.PersonId equals person.Id
                join postalCode in _context.Postalcodes on client.PostalCodeId equals postalCode.Id
                where postalCode.CityId == 5 && (employee.Id == 11 || employee.Id == 30)
                select new
                {
                    ClientName = client.ClientName,
                      City = postalCode.City.CityName, // Reemplaza con el nombre de tu propiedad que almacena el nombre de la ciudad
                      SalesRepresentative = person.FirstName + " " + person.LastName1 // Ajusta esto según la estructura de tu entidad Person
                };

    return clients.Cast<object>().ToList();
}


/* Obtén un listado con el nombre de cada cliente y el nombre y apellido de su
representante de ventas. */

public List<object> GetClientAndSalesRepresentativeInfo()
{
    var clientInfoList = from client in _context.Clients
                        join employee in _context.Employees on client.CodEmployee equals employee.PersonId
                        join person in _context.Persons on client.PersonId equals person.Id
                        where client.CodEmployee != null // Asegúrate de considerar clientes con representantes asignados
                        select new
                        {
                            ClientName = client.ClientName,
                            SalesRepresentative = person.FirstName + " " + person.LastName1
                        };

    return clientInfoList.Cast<object>().ToList();
}

public List<string> GetClientsWithPaymentsAndSalesRepresentatives()
{
    var clientsWithPayments = from client in _context.Clients
                              join payment in _context.Payments on client.PersonId equals payment.Id
                              join employee in _context.Employees on client.CodEmployee equals employee.PersonId
                              join person in _context.Persons on client.PersonId equals person.Id
                              select $"{client.ClientName} - Representante de Ventas: {person.FirstName} {person.LastName1}";

    return clientsWithPayments.ToList();
}


/* Devuelve un listado que muestre solamente los clientes que no han
realizado ningún pago. */
public List<Client> GetClientsWithoutPayments()
{
    var clientsWithoutPayments = from client in _context.Clients
        where !_context.Payments.Any(payment => payment.Id == client.PersonId)
        select client;

    return clientsWithoutPayments.ToList();
}


/*  Devuelve el nombre de los clientes a los que no se les ha entregado a
tiempo un pedido. */
public List<string> GetClientsWithLateDeliveries()
{
    var clientsWithLateDeliveries = from order in _context.Orders
                                    where order.DeliveryDate != null && order.DeliveryDate > order.ExpectedDate
                                    select order.ClientCodeNavigation.ClientName;

    return clientsWithLateDeliveries.Distinct().ToList();
}


/*  Devuelve el nombre de los clientes que no hayan hecho pagos y el nombre
de sus representantes junto con la ciudad de la oficina a la que pertenece el
representante. */
public List<object> GetClientsWithoutPaymentsAndRepresentativesWithCity()
{
    var clientsWithoutPayments = _context.Clients
        .Where(client => !client.Payments.Any())
        .Select(client => new
        {
            ClientName = client.ClientName,
            RepresentativeName = $"{client.CodEmployeeNavigation.Person.FirstName} {client.CodEmployeeNavigation.Person.LastName1}",
            RepresentativeCity = client.CodEmployeeNavigation.Office.PostalCode.City.CityName
        })
        .ToList<object>();

    return clientsWithoutPayments;
}

/* Devuelve el nombre de los clientes que han hecho pagos y el nombre de sus
representantes junto con la ciudad de la oficina a la que pertenece el
representante. */
public List<object> GetClientsWithPaymentsAndRepresentativesWithCity()
{
    var clientsWithPayments = _context.Clients
        .Where(client => client.Payments.Any())
        .Select(client => new
        {
            ClientName = client.ClientName,
            RepresentativeName = $"{client.CodEmployeeNavigation.Person.FirstName} {client.CodEmployeeNavigation.Person.LastName1}",
            RepresentativeCity = client.CodEmployeeNavigation.Office.PostalCode.City.CityName
        })
        .ToList<object>();

    return clientsWithPayments;
}


/* Muestra el nombre de los clientes que no hayan realizado pagos junto con
el nombre de sus representantes de ventas. */
public List<object> GetClientsWithoutPaymentsAndRepresentatives()
{
    var clientsWithoutPayments = _context.Clients
        .Where(client => !client.Payments.Any())
        .Select(client => new
        {
            ClientName = client.ClientName,
            RepresentativeName = $"{client.CodEmployeeNavigation.Person.FirstName} {client.CodEmployeeNavigation.Person.LastName1}"
        })
        .ToList<object>();

    return clientsWithoutPayments;
}
/* Muestra el nombre de los clientes que hayan realizado pagos junto con el
nombre de sus representantes de ventas. */
public List<object> GetClientsWithPaymentsAndRepresentatives()
{
    var clientsWithPayments = _context.Clients
        .Where(client => client.Payments.Any())
        .Select(client => new
        {
            ClientName = client.ClientName,
            RepresentativeName = $"{client.CodEmployeeNavigation.Person.FirstName} {client.CodEmployeeNavigation.Person.LastName1}"
        })
        .ToList<object>();

    return clientsWithPayments;
}

/* Devuelve el nombre de los clientes y el nombre de sus representantes junto
con la ciudad de la oficina a la que pertenece el representante. */
public List<object> GetClientsWithRepresentativesAndOfficeCity()
{
    var clientsWithRepresentatives = _context.Clients
        .Select(client => new
        {
            ClientName = client.ClientName,
            RepresentativeName = $"{client.CodEmployeeNavigation.Person.FirstName} {client.CodEmployeeNavigation.Person.LastName1}",
            RepresentativeCity = client.CodEmployeeNavigation.Office.PostalCode.City.CityName
        })
        .ToList<object>();

    return clientsWithRepresentatives;
}











    }
}
