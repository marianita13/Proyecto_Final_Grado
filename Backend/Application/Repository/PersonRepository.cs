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
    public class PersonRepository : GenericRepository<Person>, IPerson
    {
        private readonly GardeningContext _context;

        public PersonRepository(GardeningContext context) : base(context)
        {
            _context = context;
        }
    }
}
