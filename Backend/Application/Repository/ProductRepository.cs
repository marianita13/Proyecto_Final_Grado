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

    }
}
