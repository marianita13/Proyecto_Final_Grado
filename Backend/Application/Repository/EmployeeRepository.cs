using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;

namespace Application.Repository
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployee
    {
        private readonly GardeningContext _context;

        public EmployeeRepository(GardeningContext context)
            : base(context)
        {
            _context = context;
        }
        /* Devuelve un listado con el nombre, apellidos y email de los empleados cuyo
jefe tiene un código de jefe igual a 7. */
        public async Task<IEnumerable<object>> GetEmployeesByBossCode(int bossCode)
        {
            try
            {
                var employees = await (
                    from employee in _context.Employees
                    join boss in _context.Persons on employee.ManagerCode equals boss.Id
                    where employee.ManagerCode == boss.Id && boss.Id == bossCode
                    select new
                    {
                        FirstName = employee.Person.FirstName,
                        LastName = employee.Person.LastName1,
                        Email = employee.Person.Email
                    }
                ).ToListAsync();

                return employees;
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error al obtener empleados: {ex.Message}");
                throw;
            }
        }

        /* Devuelve un listado con el nombre, apellidos y puesto de aquellos
empleados que no sean representantes de ventas. */
        public async Task<IEnumerable<object>> GetNonSalesRepresentatives()
{
    try
    {
        var nonSalesReps = await (
            from employee in _context.Employees
            join person in _context.Persons on employee.PersonId equals person.Id
            join personType in _context.Persontypes on person.PersonTypeId equals personType.Id
            where personType.TypeName != "Representante Ventas"
            select new
            {
                FirstName = person.FirstName,
                LastName = person.LastName1,
                JobTitle = personType.TypeName
            }
        ).ToListAsync();

        return nonSalesReps;
    }
    catch (Exception ex)
    {

        Console.WriteLine($"Error al obtener empleados que no son representantes de ventas: {ex.Message}");
        throw; 
    }
}

/* Devuelve el nombre del puesto, nombre, apellidos y email del jefe de la
empresa. */
public async Task<IEnumerable<object>> GetCEOInformation()
{
    var ceoInfo = await (
        from employee in _context.Employees
        where employee.ManagerCode == null 
        join person in _context.Persons on employee.PersonId equals person.Id
        join position in _context.Persontypes on person.PersonTypeId equals position.Id
        select new
        {
            JobTitle = position.TypeName,
            FirstName = person.FirstName,
            LastName = person.LastName1,
            Email = person.Email
        }
    ).ToListAsync();

    return ceoInfo;
}


/* Devuelve un listado que muestre el nombre de cada empleados, el nombre
de su jefe y el nombre del jefe de sus jefe. */
public List<object> GetEmployeeHierarchy()
{
    var employeeHierarchy = from employee in _context.Employees
                            join manager in _context.Employees on employee.ManagerCode equals manager.PersonId into managers
                            from manager in managers.DefaultIfEmpty()
                            join grandManager in _context.Employees on manager.ManagerCode equals grandManager.PersonId into grandManagers
                            from grandManager in grandManagers.DefaultIfEmpty()
                            select new
                            {
                                EmployeeName = employee.Person.FirstName + " " + employee.Person.LastName1,
                                ManagerName = manager != null ? manager.Person.FirstName + " " + manager.Person.LastName1 : null,
                                GrandManagerName = grandManager != null ? grandManager.Person.FirstName + " " + grandManager.Person.LastName1 : null
                            };

    return employeeHierarchy.Cast<object>().ToList();
}
public List<object> GetEmployeesWithManagers()
{
    var employeesWithManagers = from employee in _context.Employees
                                join manager in _context.Employees on employee.ManagerCode equals manager.PersonId into managers
                                from manager in managers.DefaultIfEmpty()
                                select new
                                {
                                    EmployeeName = employee.Person.FirstName + " " + employee.Person.LastName1,
                                    ManagerName = manager != null ? manager.Person.FirstName + " " + manager.Person.LastName1 : null
                                };

    return employeesWithManagers.Cast<object>().ToList();
}





        //TODO: CONSULTA
        public async Task<IEnumerable<object>> AlbertoSoriaEmployees()
        {
            return await (from employee in _context.Employees
            join person in _context.Persons
            on employee.PersonId equals person.Id
            where employee.ManagerCode == 3 
            select new{
                Name = person.FirstName,
                firstlastname = person.LastName1,
                secondlastname = person.LastName2,
                Email = person.Email
            }
            ).ToListAsync();
        }

        //TODO: NO SE COMO HACERLA :(
        // public async Task<IEnumerable<object>> Employees_Dont_Represent_Client()
        // {
        //     return await (from employee in _context.Employees
        //     join person in _context.Persons
        //     on employee.PersonId equals person.Id
        //     join client in _context.Clients on employee.Id equals client.CodEmployee
        //     where person.PersonTypeId == 5
        //     select new{
        //         Name = person.FirstName,
        //         Apellido = person.LastName1,
        //         Cargo = person.PersonTypeId.GetHashCode()
        //     }).ToListAsync();
        // }
        // 4. Devuelve un listado que muestre solamente los empleados que no tienen una oficina asociada.
        public async Task<IEnumerable<Employee>> GetEmployeeNoOffice()
        {
            return await (from Employee in _context.Employees
                          join office in _context.Offices on Employee.OfficeCode equals office.Id into officeGroup
                          from subOffice in officeGroup.DefaultIfEmpty()
                          where subOffice == null
                          select Employee)
                        .ToListAsync();
        }
        // 5. Devuelve un listado que muestre solamente los empleados que no tienen un cliente asociado.
        public async Task<IEnumerable<Employee>> GetEmployeeNoClient()
        {
            return await (from Employee in _context.Employees
                          join Client in _context.Clients on Employee.Id equals Client.CodEmployee into clientGroup
                          from subClient in clientGroup.DefaultIfEmpty()
                          where subClient == null
                          select Employee)
                        .ToListAsync();
        }
        // 6. Devuelve un listado que muestre solamente los empleados que no tienen un cliente asociado junto con los datos de la oficina donde trabajan.
        public async Task<IEnumerable<object>> GetEmployeeNoClientWithOffice()
        {
            return await (from employee in _context.Employees
                          join client in _context.Clients on employee.Id equals client.CodEmployee into clientGroup
                          from subClient in clientGroup.DefaultIfEmpty()
                          join office in _context.Offices on employee.OfficeCode equals office.Id
                          where subClient == null
                          select new
                          {
                              EmployeeId = employee.Id,
                              EmployeeName = employee.Extention,
                              OfficeId = office.Id,
                              OfficePhone = office.Phone,
                              OfficeAddress1 = office.AddressLine1,
                              OfficeAddress2 = office.AddressLine2
                          }).ToListAsync();
        }
        // 7. Devuelve un listado que muestre los empleados que no tienen una oficina asociada y los que no tienen un cliente asociado.
        public async Task<IEnumerable<Employee>> GetEmployeeNoOfficeNoClient()
        {
            return await (from Employee in _context.Employees
                          join office in _context.Offices on Employee.OfficeCode equals office.Id into officeGroup
                          from subOffice in officeGroup.DefaultIfEmpty()
                          join Client in _context.Clients on Employee.Id equals Client.CodEmployee into clientGroup
                          from subClient in clientGroup.DefaultIfEmpty()
                          where subClient == null && subOffice == null
                          select Employee)
                        .ToListAsync();
        }

        // 12. Devuelve un listado con los datos de los empleados que no tienen clientes asociados y el nombre de su jefe asociado.
        public async Task<IEnumerable<object>> GetEmployeeNoClientWithBoss()
        {
            return await (from employee in _context.Employees
                          join Client in _context.Clients on employee.Id equals Client.CodEmployee into clientGroup
                          from subClient in clientGroup.DefaultIfEmpty()
                          join boss in _context.Persons on employee.ManagerCode equals boss.Id
                          where subClient == null
                          select new
                          {
                              EmployeeId = employee.Id,
                              extention = employee.Extention,
                              bossName = boss.FirstName,
                              bossLastName = boss.LastName1
                          })
                        .ToListAsync();
        }
        

    }
}
