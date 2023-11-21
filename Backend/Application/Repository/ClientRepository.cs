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
    public class ClientRepository : GenericRepository<Client>, IClient
    {
        private readonly GardeningContext _context;

        public ClientRepository(GardeningContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<object>> Clients_With_Any_Audio()
        {
            return await (from client in _context.Clients 
            where _context.Payments.Any(p => p.ClienteId == client.Id)
            select new{
                Id=client.Id,
                Name = client.ClientName,
                Phone = client.Phone
            }).ToListAsync();
        }

        //TODO: CONSULTA
        public async Task<object> GetBigCreditLimit()
        {
            // return await _context.Clients.OrderByDescending(c => c.CreditLimit).FirstOrDefaultAsync();
            return await (from client in _context.Clients 
            orderby client.CreditLimit descending 
            select new{
                Id = client.Id,
                Name = client.ClientName,
                creditLimit = client.CreditLimit
            }).FirstOrDefaultAsync();
        }

        //TODO: CONSULTA
        public async Task<IEnumerable<object>> GetCreditAndPayment()
        {
            return (from Client in _context.Clients 
            where Client.CreditLimit > 
            (from pay in _context.Payments 
            where pay.ClienteId == Client.Id
            select pay.Total).Sum()
            && 
            (from pay in _context.Payments 
            where pay.ClienteId == Client.Id
            select pay.Total).Sum()>0
            select new{
                Id = Client.Id,
                name = Client.ClientName,
                creditLimit = Client.CreditLimit,
                totalpayment = (from pay in _context.Payments
                where pay.ClienteId == Client.Id select pay.Total).Sum()
            });
        }
    }
}
