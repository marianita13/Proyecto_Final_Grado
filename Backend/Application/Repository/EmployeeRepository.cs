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

        public EmployeeRepository(GardeningContext context)
            : base(context)
        {
            _context = context;
        }

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
                // Manejar la excepción según tus necesidades (puedes registrarla o mostrarla en la salida)
                Console.WriteLine($"Error al obtener empleados: {ex.Message}");
                throw; // Puedes elegir relanzar la excepción o manejarla de manera diferente
            }
        }
    }
}
