using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions;
using Model.Entities;
using Model.Repositories;

namespace Application.Services {
    public class SizeService : ISizeService {
        private readonly SizeRepository _sizeRepository;
        public SizeService(SizeRepository sizeRepository) {
            _sizeRepository = sizeRepository;
        }
        public async Task AddAsync(Size entity) {
            _sizeRepository.Add(entity);
            await _sizeRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Size>> GetAllAsync() {
            return await _sizeRepository.GetAllAsync();
        }

        public async Task<Size> GetAsync(int id) {
            return await _sizeRepository.GetAsync(id);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var size = await _sizeRepository.GetAsync(id);
            _sizeRepository.Delete(size);
            await _sizeRepository.SaveChangesAsync();
        }
    }
}
