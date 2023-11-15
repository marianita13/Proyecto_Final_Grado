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
    public class Method_PaymentRepository : GenericRepository<MethodPayment>, IMethod_Payment
    {
        private readonly GardeningContext _context;

        public Method_PaymentRepository(GardeningContext context) : base(context)
        {
            _context = context;
        }
    }
}
