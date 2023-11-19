using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace Application.Repository
{
    public class BossRepository : GenericRepository<Boss>, IBoss
    {
        private readonly GardeningContext _context;
        public BossRepository(GardeningContext context) : base(context)
        {
            _context = context;
        }
    }
}