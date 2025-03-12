using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions;
using Model.Entities;
using Model.Repositories;

namespace Application.Services {
    public class BrandService : IBrandService {
        private readonly BrandRepository _brandRepository;
        private readonly UserRepository _userRepository;
        public BrandService(BrandRepository brandRepository, UserRepository userRepository) {
            _brandRepository = brandRepository;
            _userRepository = userRepository;
        }
        public async Task AddAsync(Brand entity) {
            var brand = await _brandRepository.GetByName(entity.Name);
            if (brand != null) {
                throw new Exception("Brand already exists");
            }
            var user = await _userRepository.GetAsync(entity.AddedBy);
            if (user.Role != "Admin")
            {
                throw new Exception("Only Admin can add a brand");
            }
            _brandRepository.Add(entity);
            await _brandRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Brand>> GetAllAsync() {
            return await this._brandRepository.GetAllAsync();
        }

        public async Task<Brand> GetAsync(int id) {
            return await this._brandRepository.GetAsync(id);
        }

    }
}
