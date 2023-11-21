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
public async Task<IEnumerable<object>> GetPaymentsIn2008ByPaypal()
{
    var paymentsIn2008 = await (
        from payment in _context.Payments
        where payment.PaymentDate.Year == 2008 && payment.Method.MethodPayment1 == "Paypal"
        orderby payment.Total descending
        select new
        {
            Id = payment.Id,
            PaymentDate = payment.PaymentDate,
            MethodId = payment.MethodId, 
            Method = payment.Method.MethodPayment1, 
        }
    ).ToListAsync();

    return paymentsIn2008;
}

 /* Devuelve un listado con todas las formas de pago que aparecen en la
tabla pago. Tenga en cuenta que no deben aparecer formas de pago
repetidas */
public async Task<IEnumerable<object>> GetDistinctPaymentMethods()
{
    return await (from Method in _context.Methodpayments
    select new{
        name = Method.MethodPayment1
    }).ToListAsync();
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
