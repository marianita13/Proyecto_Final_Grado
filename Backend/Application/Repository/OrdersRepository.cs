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
    public class OrdersRepository : GenericRepository<Order>, IOrders
    {
        private readonly GardeningContext _context;

        public OrdersRepository(GardeningContext context) : base(context)
        {
            _context = context;
        }

        /* Devuelve un listado con los distintos estados por los que puede pasar un
pedido. */
        public async Task<IEnumerable<object>> GetOrderStatusList()
{
    try
    {
        return await (from status in _context.Statuses
        select new{
            name = status.NameStatus
        }).ToListAsync();
    }
    catch (Exception ex)
    {

        Console.WriteLine($"Error al obtener los distintos estados de los pedidos: {ex.Message}");
        throw; 
    }
}

/* Devuelve un listado con el código de pedido, código de cliente, fecha
esperada y fecha de entrega de los pedidos que no han sido entregados a
tiempo. */
public async Task<IEnumerable<object>> GetDelayedOrders()
{
    var delayedOrders = await (
        from order in _context.Orders
        where order.DeliveryDate.HasValue && order.DeliveryDate > order.ExpectedDate
        select new{
            orderId = order.Id,
            clientId = order.ClientCode,
            ExpectedDate = order.ExpectedDate,
            DeliveredDate = order.DeliveryDate,
            Comments = order.Comments
        }
    ).ToListAsync();

    return delayedOrders;
}

/* Devuelve un listado con el código de pedido, código de cliente, fecha
esperada y fecha de entrega de los pedidos cuya fecha de entrega ha sido al
menos dos días antes de la fecha esperada. */
public async Task<IEnumerable<object>> GetOrdersDeliveredEarly()
{
    var earlyDeliveredOrders = await (
        from order in _context.Orders
        where EF.Functions.DateDiffDay(order.DeliveryDate, order.ExpectedDate) >= 2
        select new
        {
            Id = order.Id,
            ClientCode = order.ClientCode,
            ExpectedDate = order.ExpectedDate,
            DeliveryDate = order.DeliveryDate.Value
        }
    ).ToListAsync();

    return earlyDeliveredOrders;
}

/* Devuelve un listado de todos los pedidos que fueron rechazados en 2009. */
public async Task<IEnumerable<object>> GetRejectedOrdersIn2009()
{
    return await (
        from order in _context.Orders
        where order.StatusCode == 2 // Reemplaza 'RejectedStatusCode' con el código correspondiente para pedidos rechazados en tu sistema
            && order.OrderDate.Year == 2009
        select new
        {
            Id = order.Id,
            ClientCode = order.ClientCode,
            OrderDate = order.OrderDate,
            StatusCode = order.StatusCode
        }
    ).ToListAsync();

}

/* Devuelve un listado de todos los pedidos que han sido entregados en el
mes de enero de cualquier año. */
public async Task<IEnumerable<object>> GetOrdersDeliveredInJanuary()
{
    var ordersInJanuary = await (
        from order in _context.Orders
        where order.DeliveryDate != null && order.DeliveryDate.Value.Month == 1
        select new
        {
            Id = order.Id, 
            ClientCode = order.ClientCode,
            ExpectedDate = order.ExpectedDate,
            DeliveryDate = order.DeliveryDate.Value
        }
    ).ToListAsync();

    return ordersInJanuary;
}









    }
}
