using Microsoft.EntityFrameworkCore;
using Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repositories {
    public abstract class GenericRepository <T> where T:class {
        public readonly MyDbContext _context;
        public GenericRepository(MyDbContext context) {
            _context = context;
        }

        public async Task<List<T>> GetAllAsync() {
            return await _context.Set<T>().ToListAsync();
        }

        public void Add(T entity) {
            _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
        }

        public void Delete(T entity) {
            _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        }

        public void Update(T entity) {
            _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public async Task<T> GetAsync(int id) {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

