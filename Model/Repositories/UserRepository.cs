using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model.Context;
using Model.Entities;

namespace Model.Repositories {
    public class UserRepository : GenericRepository<User>{
        public UserRepository(MyDbContext context) : base(context) { }
        public async Task<User> GetByEmail(string email) {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }
    }
}
