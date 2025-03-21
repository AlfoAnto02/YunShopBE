using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.Models.Request;
using Microsoft.Identity.Client;
using Model.Entities;
using Model.Repositories;

namespace Application.Services {
    public class ProductService : IProductService {
        private readonly ProductRepository _productRepository;
        private readonly IImageService _imageService;
        private readonly ISizeService _sizeService;
        private readonly IProductSizeService productSizeService;
        public ProductService(ProductRepository productRepository,IImageService imageService, ISizeService _service, IProductSizeService productSizeService) {
            _productRepository = productRepository;
            _imageService = imageService;
            _sizeService = _service;
            this.productSizeService = productSizeService;
        }

        public async Task AddAsync(Product entity, List<AddProductSizeRequest> sizes)
        {
            _productRepository.Add(entity);
            await _productRepository.SaveChangesAsync();
            foreach (var image in entity.Images)
            {
                image.ProductId = entity.Id;
                await _imageService.AddAsync(image);
            }
            await productSizeService.AddRelationsAsync(entity.Id,sizes);
        }


        public async Task<IEnumerable<Product>> GetAllAsync() {
            var products = await _productRepository.GetProductsWithImages();
            if (products.Count == 0)
            {
                throw new Exception("No products found");
            }
            return products;
        }
    
        public async Task<Product> GetAsync(int id) {
            var product = await _productRepository.GetAsync(id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            return product;
        }

        public async Task DeleteAsync(int productId, int userId)
        {
            _productRepository.Delete(await _productRepository.GetAsync(productId));
            await _productRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int productId, Product product)
        {
            var productToUpdate = await _productRepository.GetAsync(productId);
            var imagesToRemove = productToUpdate.Images.Where(x => !product.Images.Select(y => y.Id).Contains(x.Id)).ToList();
            foreach (var image in imagesToRemove)
            {
                await _imageService.DeleteAsync(image.Id);
            } 

            if (product.CategoryId != productToUpdate.CategoryId)
            {
                productToUpdate.CategoryId = product.CategoryId;
            }
            productToUpdate.Name = product.Name;
            productToUpdate.BrandId = product.BrandId;
            productToUpdate.Description = product.Description;
            productToUpdate.UpdatedAt = DateTime.Now;
            foreach (var image in product.Images)
            {
                if (image.Id == 0)
                {
                    image.ProductId = productToUpdate.Id;
                    await _imageService.AddAsync(image);
                }
            }
            _productRepository.Update(productToUpdate);
            await _productRepository.SaveChangesAsync();

        }

        public Task AddAsync(Product entity) {
            throw new NotImplementedException();
        }
    }
}
