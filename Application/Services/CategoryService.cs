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
    public class CategoryService : ICategoryService {
        public readonly CategoryRepository _categoryRepository;
        public readonly UserRepository _userRepository;
        public CategoryService(CategoryRepository categoryRepository, UserRepository userRepository) {
            _categoryRepository = categoryRepository;
            this._userRepository = userRepository;
        }
        public async Task AddAsync(Category entity) {
            this._categoryRepository.Add(entity);
            await this._categoryRepository.SaveChangesAsync();

        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await this._categoryRepository.GetAllAsync();
        }

        public async Task<Category> GetAsync(int id) {
            var category = await this._categoryRepository.GetAsync(id);
            if (category == null) {
                throw new Exception("Wrong Category Id: "+id);
            }

            return category;
        }

        public async Task DeleteAsync(DeleteCategoryRequest deleteRequest) {
            var category = await this._categoryRepository.GetByName(deleteRequest.Name);
            this._categoryRepository.Delete(category);
            await this._categoryRepository.SaveChangesAsync();
        }
    }
}
