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
    public class EmployeeRepository : GenericRepository<Employee>, IEmployee
    {
        private readonly GardeningContext _context;

        public EmployeeRepository(GardeningContext context) : base(context)
        {
            _context = context;
        }
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
