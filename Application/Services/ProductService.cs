using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions;
using Model.Entities;
using Model.Repositories;

namespace Application.Services {
    public class ProductService : IProductService {
        private readonly ProductRepository _productRepository;
        private readonly UserRepository _userRepository;
        private readonly IImageService _imageService;
        private readonly CategoryRepository _categoryRepository;
        public ProductService(ProductRepository productRepository,IImageService imageService, UserRepository userRepository, 
            CategoryRepository categoryRepository) {
            _productRepository = productRepository;
            _userRepository = userRepository;
            _imageService = imageService;
            _categoryRepository = categoryRepository;
        }
        public async Task AddAsync(Product entity) {
            switch (entity) {
                case { Stock: <= 0 }:
                    throw new Exception("Stock must be greater than 0");
                case { Price: <= 0 }:
                    throw new Exception("Price must be greater than 0");
                case { UserId: 0 }:
                    throw new Exception("User Id must be greater than 0");
            }
            var user = await _userRepository.GetAsync(entity.UserId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            if (user.Role != "Admin")
            {
                throw new Exception("User must be an admin");
            }

            var category = await _categoryRepository.GetAsync(entity.CategoryId);
            if (category == null)
            {
                throw new Exception("Category not found");
            }
            _productRepository.Add(entity);
            await _productRepository.SaveChangesAsync();
            foreach (var image in entity.Images)
            {
                image.ProductId = entity.Id;
                await _imageService.AddAsync(image);
            }
        }

        public async Task<IEnumerable<Product>> GetAllAsync() {
            var products = await _productRepository.GetAllAsync();
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
    }
}
