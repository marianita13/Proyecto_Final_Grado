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
    public class PaymentRepository : GenericRepository<Payment>, IPayment
    {
        private readonly GardeningContext _context;

        public PaymentRepository(GardeningContext context) : base(context)
        {
            _context = context;
        }
    }
}
