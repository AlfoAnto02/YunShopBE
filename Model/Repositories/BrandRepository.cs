using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model.Context;
using Model.Entities;

namespace Model.Repositories {
    public class BrandRepository : GenericRepository<Brand> {
        public BrandRepository(MyDbContext context) : base(context) { }

        public async Task<Brand> GetByName(string name)
        {
            return await _context.Brands.FirstOrDefaultAsync(x => x.Name == name);
        }

    }
}
