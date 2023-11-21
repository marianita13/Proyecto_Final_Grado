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
    }
}
