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
    public class ProductRepository : GenericRepositoryS<Product>, IProduct
    {
        private readonly GardeningContext _context;

        public ProductRepository(GardeningContext context) : base(context)
        {
            _context = context;
        }
        // 8. Devuelve un listado de los productos que nunca han aparecido en un pedido.
        public async Task<IEnumerable<Product>> GetProductsNoOrder()
        {
            return await (from product in _context.Products
                            join OrderDetail in _context.Orderdetails on product.Id equals OrderDetail.ProductCode into OrderDetailGroup
                            from subOrderDetail in OrderDetailGroup.DefaultIfEmpty()
                            where subOrderDetail == null
                            select product)
                            .ToListAsync();
        }
        // 9. Devuelve un listado de los productos que nunca han aparecido en un pedido. El resultado debe mostrar el nombre, la descripción y la imagen del producto.
        public async Task<IEnumerable<object>> GetProductsNoOrderWithInfo()
        {
            return await (from product in _context.Products
                            join OrderDetail in _context.Orderdetails on product.Id equals OrderDetail.ProductCode into OrderDetailGroup
                            from subOrderDetail in OrderDetailGroup.DefaultIfEmpty()
                            join ProductLine in _context.Productlines on product.ProductLine equals ProductLine.Id
                            where subOrderDetail == null
                            select new {
                                nombre = product.Name,
                                description = product.Description,
                                image = ProductLine.Image
                                })
                            .ToListAsync();
        }



/* Devuelve un listado con todos los productos que pertenecen a la
gama Ornamentales y que tienen más de 100 unidades en stock. El listado
deberá estar ordenado por su precio de venta, mostrando en primer lugar
los de mayor precio */
        public async Task<IEnumerable<Product>> GetOrnamentalProductsOver100Stock()
{
    var products = await _context.Products
        .Where(product => product.ProductLineNavigation.ProductLine1 == "Ornamentales" && product.StockQuantity > 100)
        .OrderByDescending(product => product.SellingPrice)
        .Select(product => new Product
        {
            Id = product.Id, // Asumiendo que el código de producto es la propiedad 'Id'
            Name = product.Name,
            ProductLine = product.ProductLine,
            StockQuantity = product.StockQuantity,
            SellingPrice = product.SellingPrice
        })
        .ToListAsync();

    return products;
}

/*  Devuelve un listado de las diferentes gamas de producto que ha comprado
cada cliente. */

public List<object> GetProductRangesPerClient()
{
    var productRangesPerClient = from order in _context.Orders
                                 from orderDetail in _context.Orderdetails
                                 from product in _context.Products
                                 from productLine in _context.Productlines
                                 where order.Id == orderDetail.OrderId && orderDetail.ProductCode == product.Id && product.ProductLineNavigation.ProductLine1 == productLine.ProductLine1
                                 group new { productLine.DescriptionText, product.ProductLineNavigation.ProductLine1 } by new { order.ClientCodeNavigation.PersonId, order.ClientCodeNavigation.ClientName } into groupedProducts
                                 select new
                                 {
                                     ClientName = groupedProducts.Key.ClientName,
                                     PersonId = groupedProducts.Key.PersonId,
                                     ProductRanges = groupedProducts.Select(p => new { p.DescriptionText, p.ProductLine1 }).Distinct().ToList()
                                 };

    return productRangesPerClient.ToList<object>();
}













        public async Task<IEnumerable<object>> AnyProduct()
        {
            return await (from product in _context.Products
            where !_context.Orderdetails.Any(p => p.ProductCode == product.Id)
            select new{
                Id = product.Id,
                name = product.Name,
                

            }).ToListAsync();
        }

        //TODO: CONSULTA
        public async Task<object> GetExpensiveProduct()
        {
            // return await _context.Products.OrderByDescending(e => e.SellingPrice).FirstOrDefaultAsync();
            return await (from product in _context.Products 
            orderby product.SellingPrice descending 
            select new{
                Id = product.Id,
                Name = product.Name,
                price = product.SellingPrice
            }).FirstOrDefaultAsync();
        }

        //TODO: CONSULTA
        public async Task<object> GetProductHigherStock()
        {
            return await (from product in _context.Products
            orderby product.StockQuantity descending
            select new{
                Id = product.Id,
                Name = product.Name,
                Quantity = product.StockQuantity,
                Descripcion = product.Description
            }).FirstOrDefaultAsync();
        }

        //TODO: CONSULTA
        public async Task<object> GetProductLowerStock()
        {
            return await (from product in _context.Products
            orderby product.StockQuantity
            select new{
                Id = product.Id,
                Name = product.Name,
                Quantity = product.StockQuantity,
                Descripcion = product.Description
            }).FirstOrDefaultAsync();
        }

/* Lista las ventas totales de los productos que hayan facturado más de 3000
euros. Se mostrará el nombre, unidades vendidas, total facturado y total
facturado con impuestos (21% IVA) */
public List<object> GetProductsSalesOver3000Euros()
{
    var productsSalesOver3000Euros = _context.Products
        .SelectMany(product => product.Orderdetails)
        .Where(orderDetail => (orderDetail.Order.StatusCode == 1 || orderDetail.Order.StatusCode == 3) && 
                              orderDetail.UnitPrice * orderDetail.Quantity > 3000)
        .GroupBy(orderDetail => new
        {
            ProductName = orderDetail.Product.Name,
            UnitsSold = orderDetail.Quantity,
            TotalSales = orderDetail.UnitPrice * orderDetail.Quantity,
            TotalSalesWithIVA = orderDetail.UnitPrice * orderDetail.Quantity * 1.21m 
        })
        .Select(groupedResult => new
        {
            ProductName = groupedResult.Key.ProductName,
            UnitsSold = groupedResult.Key.UnitsSold,
            TotalSales = groupedResult.Key.TotalSales,
            TotalSalesWithIVA = groupedResult.Key.TotalSalesWithIVA
        })
        .ToList<object>();

    return productsSalesOver3000Euros;
}

public List<string> GetProductWithMostUnitsSold()
{
    var productsWithMostUnitsSold = (from orderDetail in _context.Orderdetails
                                     group orderDetail by orderDetail.Product.Name into groupedResult
                                     orderby groupedResult.Sum(od => od.Quantity) descending
                                     select $"{groupedResult.Key} - Total Units Sold: {groupedResult.Sum(od => od.Quantity)}")
                                    .Take(1)
                                    .ToList();

    return productsWithMostUnitsSold;
}







    }
}
