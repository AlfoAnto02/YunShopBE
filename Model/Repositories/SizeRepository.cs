using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model.Context;
using Model.Entities;

namespace Model.Repositories {
    public class SizeRepository : GenericRepository<Size> {
        public SizeRepository(MyDbContext context) : base(context) { }

        public Task<Size> GetByName(string sizeValue) {
            return _context.Sizes.FirstOrDefaultAsync(x => x.SizeValue == sizeValue);
        }
    }
}
