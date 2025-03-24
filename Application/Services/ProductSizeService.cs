using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.Models.Request;
using Model.Entities;
using Model.Repositories;

namespace Application.Services {
    public class ProductSizeService : IProductSizeService {
        private readonly ProductSizeRepository _productSizeRepository;
        private readonly ISizeService _sizeService;
        public ProductSizeService(ProductSizeRepository productSizeRepository, ISizeService sizeService) {
            this._productSizeRepository = productSizeRepository;
            this._sizeService = sizeService;
        }

        public async Task AddAsync(ProductSize entity) {
            _productSizeRepository.Add(entity);
            await _productSizeRepository.SaveChangesAsync();
        }

        public async Task AddRelationsAsync(int entityId, List<AddProductSizeRequest> sizes) {
            var productSizes = new List<ProductSize>();
            foreach (var ids in sizes) {
                var size = await _sizeService.GetAsync(ids.SizeId);
                productSizes.Add(new ProductSize {
                    ProductId = entityId,
                    SizeId = size.Id,
                    Stock = ids.Stock,
                    Price = ids.Price,
                    Express = ids.Express,
                    Hide = ids.Hide,
                });
            }
            _productSizeRepository.AddRange(productSizes);
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
