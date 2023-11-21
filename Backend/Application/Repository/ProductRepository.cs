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
