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












    }
}
