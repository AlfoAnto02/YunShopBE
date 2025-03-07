﻿using Application.Abstractions;
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
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoryController : ControllerBase {
        public readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService) {
            _categoryService = categoryService;
        }
        [HttpPost("add")]
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
        [HttpGet("getAll")]
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
        [HttpGet("getById")]
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

        [HttpDelete("delete")]
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
