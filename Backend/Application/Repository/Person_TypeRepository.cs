using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistence;
using Persistence.Data;

namespace Application.Repository
{
    public class Person_TypeRepository : GenericRepository<PersonType>, IPerson_Type
    {
        private readonly GardeningContext _context;

        public Person_TypeRepository(GardeningContext context) : base(context)
        {
            _context = context;
        }
    }
}
