using Application.Abstractions;
using Application.Models.DTOs;
using Application.Models.Request;
using Application.Models.Responses;
using Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;

namespace YunShopBE.Controllers {
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoriesController : ControllerBase {
        public readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService) {
            _categoryService = categoryService;
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] AddCategoryRequest categoryRequest) {
            try {
                var category = categoryRequest.ToEntity();
                await _categoryService.AddAsync(category);
                return Ok(ResponseFactory.WithSuccess("Category Added!"));
            }
            catch (Exception e) {
                return BadRequest(ResponseFactory.WithError(e));
            }
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategories() {
            try {
                var categories = await _categoryService.GetAllAsync();
                var categoriesDTOs = categories.Select(c => new CategoryDTO(c)).ToList();
                return Ok(ResponseFactory.WithSuccess(categoriesDTOs));
            }
            catch (Exception e) {
                return BadRequest(ResponseFactory.WithError(e));
            }
        }
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategoryById(int id) {
            try {
                var category = await _categoryService.GetAsync(id);
                var response = new CategoryResponse
                {
                    Category = new CategoryDTO(category)
                };
                return Ok(ResponseFactory.WithSuccess(category));
            }
            catch (Exception e) {
                return BadRequest(ResponseFactory.WithError(e));
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory([FromBody] DeleteCategoryRequest deleteCategoryRequest) {
            try {
                await _categoryService.DeleteAsync(deleteCategoryRequest);
                return Ok(ResponseFactory.WithSuccess("Category Deleted!"));
            }
            catch (Exception e) {
                return BadRequest(ResponseFactory.WithError(e));
            }
        }
    }
}
