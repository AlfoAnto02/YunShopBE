using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions;
using Model.Entities;
using Model.Repositories;

namespace Application.Services {
    public class ProductSizeService : IProductSizeService {
        private readonly ProductSizeRepository _productSizeRepository;
        public ProductSizeService(ProductSizeRepository productSizeRepository) {
            this._productSizeRepository = productSizeRepository;
        }

        public async Task AddAsync(ProductSize entity) {
            _productSizeRepository.Add(entity);
            await _productSizeRepository.SaveChangesAsync();
        }

        public async Task AddRelationsAsync(List<ProductSize> productSizes) {
            foreach (var productSize in productSizes) {
                await AddAsync(productSize);
            }
        }

        public async Task<IEnumerable<ProductSize>> GetAllAsync()
        {
            return await _productSizeRepository.GetAllAsync();
        }

        public async Task<ProductSize> GetAsync(int id) {
            return await _productSizeRepository.GetAsync(id);
        }
    }
}
