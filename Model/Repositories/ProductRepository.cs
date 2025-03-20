using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Model.Context;
using Microsoft.EntityFrameworkCore;

namespace Model.Repositories {
    public class ProductRepository : GenericRepository<Product>{
        public ProductRepository(MyDbContext context) : base(context) {
        }

        public async Task<Product> GetByName(string name) {
            return await _context.Products.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<List<Product>> GetProductsWithImages()
        {
            var products= await _context.Products.Include(p => p.Images).ToListAsync();
            return products;
        }
    }
}
