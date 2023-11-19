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
    public class Order_detailRepository : GenericRepository<OrderDetail>, IOrder_detail
    {
        private readonly GardeningContext _context;

        public Order_detailRepository(GardeningContext context) : base(context)
        {
            _context = context;
        }
    }
}
