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
    public class Product_lineRepository : GenericRepository<ProductLine>, IProduct_line
    {
        private readonly GardeningContext _context;

        public Product_lineRepository(GardeningContext context) : base(context)
        {
            _context = context;
        }

    }
}
