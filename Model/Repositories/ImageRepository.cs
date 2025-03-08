using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model.Context;
using Model.Entities;

namespace Model.Repositories {
    public class ImageRepository : GenericRepository<Image> {
        public ImageRepository(MyDbContext context) : base(context) {
            
        }

        public async Task<IEnumerable<Image>> GetImagesByProductId(int productId) {
            return await _context.Images.Where(x => x.ProductId == productId).ToListAsync();
        }

        public async Task<Image> GetImageByUrl(string url)
        {
            return await _context.Images.FirstOrDefaultAsync(x => x.Url == url);
        }

    }
}
