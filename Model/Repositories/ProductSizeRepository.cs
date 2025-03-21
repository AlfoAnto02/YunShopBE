using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model.Context;
using Model.Entities;

namespace Model.Repositories {
    public class ProductSizeRepository : GenericRepository<ProductSize> {
        public ProductSizeRepository(MyDbContext context) : base(context) {
        }

        public async Task<List<ProductSize>> GetProductSizesWithProduct()
        {
            var productSizes = await _context.ProductSizes.Include(p => p.Product).ToListAsync();
            return productSizes;
        }

        public void AddRange(IEnumerable<ProductSize> entities) {
            _context.ProductSizes.AddRange(entities);
        }
    }
}
