
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Interfaces;
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
        // 1. Devuelve un listado que muestre solamente los clientes que no han realizado ningún pago.
        public async Task<IEnumerable<Client>> GetClientNoPayment()
        {
            return await (from client in _context.Clients
                            join payment in _context.Payments on client.Id equals payment.ClienteId into JoinedData
                            from subpayment in JoinedData.DefaultIfEmpty()
                            where subpayment == null
                            select client)
                        .ToListAsync();
        }
        // 2. Devuelve un listado que muestre solamente los clientes que no han realizado ningún pedido.
        public async Task<IEnumerable<Client>> GetClientNoOrder()
        {
            return await (from client in _context.Clients
                            join order in _context.Orders on client.Id equals order.ClientCode into JoinedData
                            from subOrder in JoinedData.DefaultIfEmpty()
                            where subOrder == null
                            select client)
                        .ToListAsync();
        }
        // 3. Devuelve un listado que muestre los clientes que no han realizado ningún pago y los que no han realizado ningún pedido.
        public async Task<IEnumerable<Client>> GetClientNoPaymentNoOrder()
        {
            return await (from client in _context.Clients
                            join order in _context.Orders on client.Id equals order.ClientCode into orderGroup
                            from order in orderGroup.DefaultIfEmpty()
                            join payment in _context.Payments on client.Id equals payment.ClienteId into paymentGroup
                            from payment in paymentGroup.DefaultIfEmpty()
                            where payment == null && order == null
                            select client).ToListAsync();
        }

        // 11. Devuelve un listado con los clientes que han realizado algún pedido pero no han realizado ningún pago.
        public async Task<IEnumerable<Client>> GetClientNoPaymentYesOrder()
        {
            return await (from client in _context.Clients
                            join payment in _context.Payments on client.Id equals payment.ClienteId into paymentGroup
                            from subpayment in paymentGroup.DefaultIfEmpty()
                            join order in _context.Orders on client.Id equals order.ClientCode into orderGroup
                            from subOrder in orderGroup.DefaultIfEmpty()
                            where subpayment == null
                            select client)
                        .ToListAsync();
        }
    }
    }
