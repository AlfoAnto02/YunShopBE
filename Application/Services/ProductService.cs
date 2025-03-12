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
        private readonly IUserService _userService;
        private readonly IImageService _imageService;
        private readonly ICategoryService _categoryService;
        public ProductService(ProductRepository productRepository,IImageService imageService, IUserService userService, 
            ICategoryService categoryService) {
            _productRepository = productRepository;
            _userService = userService;
            _imageService = imageService;
            _categoryService = categoryService;
        }
        public async Task AddAsync(Product entity)
        {
            CheckEntity(entity);
            var user = await _userService.GetAsync(entity.UserId);
            CheckUser(user);
            var category = await _categoryService.GetAsync(entity.CategoryId);
            CheckCategory(category);
            //_imageService.CheckImages(entity.Images);
            _productRepository.Add(entity);
            foreach (var image in entity.Images)
            {
                image.ProductId = entity.Id;
                await _imageService.AddAsync(image);
            }
            await _productRepository.SaveChangesAsync();
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
            var product = await _productRepository.GetAsync(productId);
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            CheckUser(await _userService.GetAsync(userId));
            _productRepository.Delete(product);
            await _productRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int productId, Product product)
        {
            CheckEntity(product);
            CheckUser(await _userService.GetAsync(product.UserId));
            var category = await _categoryService.GetAsync(product.CategoryId);
            CheckCategory(category);
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
            productToUpdate.Brand = product.Brand;
            productToUpdate.Price = product.Price;
            productToUpdate.Stock = product.Stock;
            productToUpdate.Size = product.Size;
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

        private void CheckEntity(Product entity) {
            switch (entity) {
                case { Stock: <= 0 }:
                    throw new Exception("Stock must be greater than 0");
                case { Price: <= 0 }:
                    throw new Exception("Price must be greater than 0");
                case { UserId: 0 }:
                    throw new Exception("User Id must be greater than 0");
            }
        }

        private void CheckUser(User user) {
            if (user == null) {
                throw new Exception("User not found");
            }
            if (user.Role != "Admin") {
                throw new Exception("User must be an admin");
            }
        }

        private void CheckCategory(Category category) {
            if (category == null) {
                throw new Exception("Category not found");
            }
        }
    }
}
