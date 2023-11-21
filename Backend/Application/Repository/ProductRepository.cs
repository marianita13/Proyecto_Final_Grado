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
    }
}
