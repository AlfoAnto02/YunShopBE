using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model.Context;
using Model.Entities;

namespace Model.Repositories {
    public class CategoryRepository : GenericRepository<Category> {
        public CategoryRepository(MyDbContext context) : base(context) {
        }
        public async Task<Category> GetByName(string name) {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower());
        }
        
    }
}
