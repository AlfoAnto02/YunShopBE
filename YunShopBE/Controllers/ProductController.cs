using Application.Abstractions;
using Application.Models.DTOs;
using Application.Models.Request;
using Application.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace YunShopBE.Controllers {
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController : ControllerBase{
        private readonly IProductService _productService;
        public ProductController(IProductService productService) {
            _productService = productService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync() {
            try
            {
                var products = await _productService.GetAllAsync();
                var productsDTOs = products.Select(p => new ProductDTO(p)).ToList();
                return Ok(ResponseFactory.WithSuccess(productsDTOs));
            }
            catch (Exception e)
            {
                return BadRequest(ResponseFactory.WithError(e));
            }
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetByIdAsync(int id) {
            try
            {
                var product = await _productService.GetAsync(id);
                var response = new ProductResponse
                {
                    Product = new ProductDTO(product)
                };
                return Ok(ResponseFactory.WithSuccess(response));
            }
            catch (Exception e)
            {
                return BadRequest(ResponseFactory.WithError(e));
            }
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddProductsAsync([FromBody] AddProductsRequest addProductRequest) {
            try
            {
                var product = addProductRequest.ToEntity();
                await _productService.AddAsync(product, addProductRequest.Sizes);
                return Ok(ResponseFactory.WithSuccess("Product Added!"));
            }
            catch (Exception e)
            {
                return BadRequest(ResponseFactory.WithError(e));
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteAsync([FromBody] DeleteProductRequest deleteProductRequest)
        {
            try
            {
                await _productService.DeleteAsync(deleteProductRequest.ProductId, deleteProductRequest.UserId);
                return Ok(ResponseFactory.WithSuccess("Product Deleted!"));
            }
            catch (Exception e)
            {
                return BadRequest(ResponseFactory.WithError(e));
            }

        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateProductRequest updateProductRequest)
        {
            try
            {
                await _productService.UpdateAsync(updateProductRequest.Id, updateProductRequest.Product.ToEntity());
                return Ok(ResponseFactory.WithSuccess("Product Updated!"));
            }
            catch (Exception e)
            {
                return BadRequest(ResponseFactory.WithError(e));
            }
        }
    }
}
