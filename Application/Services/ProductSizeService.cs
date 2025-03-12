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
        private readonly IProductService _productService;
        private readonly ISizeService _sizeService;
        private readonly ProductSizeRepository _productSizeRepository;
        public ProductSizeService(IProductService productService, ISizeService sizeService, ProductSizeRepository productSizeRepository) {
            this._productService = productService;
            this._sizeService = sizeService;
            this._productSizeRepository = productSizeRepository;
        }

        public async Task AddAsync(ProductSize entity) {
            var product = await _productService.GetAsync(entity.ProductId);
            var size = await _sizeService.GetAsync(entity.SizeId);
            product.ProductSizes.Add(new ProductSize { Product = product, Size = size });
            _productSizeRepository.Add(entity);
            await _productService.UpdateAsync(entity.ProductId, product);
            await _productSizeRepository.SaveChangesAsync();
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
