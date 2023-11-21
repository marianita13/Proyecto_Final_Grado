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

/*  Devuelve un listado con todos los pagos que se realizaron en el
año 2008 mediante Paypal. Ordene el resultado de mayor a menor */
public async Task<IEnumerable<Payment>> GetPaymentsIn2008ByPaypal()
{
    var paymentsIn2008 = await (
        from payment in _context.Payments
        where payment.PaymentDate.Year == 2008 && payment.Method.MethodPayment1 == "Paypal"
        orderby payment.Total descending
        select new Payment
        {
            Id = payment.Id,
            PaymentDate = payment.PaymentDate,
            Total = payment.Total,
            ClienteId = payment.ClienteId, 
            MethodId = payment.MethodId, 
            Method = payment.Method, 
        }
    ).ToListAsync();

    return paymentsIn2008;
}

 /* Devuelve un listado con todas las formas de pago que aparecen en la
tabla pago. Tenga en cuenta que no deben aparecer formas de pago
repetidas */
public async Task<IEnumerable<string>> GetDistinctPaymentMethods()
{
    var distinctPaymentMethods = await _context.Payments
        .Select(payment => payment.Method.MethodPayment1)
        .Distinct()
        .ToListAsync();

    return distinctPaymentMethods;
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
