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
    public class PaymentRepository : GenericRepository<Payment>, IPayment
    {
        private readonly GardeningContext _context;

        public PaymentRepository(GardeningContext context) : base(context)
        {
            _context = context;
        }

        //TODO: CONSULTA
        public async Task<IEnumerable<object>> GetPaymentsForYear()
        {
            return await (from payment in _context.Payments
                        group payment by payment.PaymentDate.Year into grupo
                        select new {
                            year = grupo.Key,
                            Total = grupo.Sum(p => p.Total)
                        }).ToListAsync();
        }
    }
}
