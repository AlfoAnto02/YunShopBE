using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Context;

namespace Model.Repositories {
    public class ProductRepository : GenericRepository<Product>{
        public ProductRepository(MyDbContext context) : base(context) {
        }
    }
}
