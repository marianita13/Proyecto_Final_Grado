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
    public class ClientRepository : GenericRepository<Client>, IClient
    {
        private readonly GardeningContext _context;

        public ClientRepository(GardeningContext context) : base(context)
        {
            _context = context;
        }
    }
}
