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
        public async Task<IActionResult> AddAsync([FromBody] AddProductRequest addProductRequest) {
            try
            {
                var product = addProductRequest.ToEntity();
                await _productService.AddAsync(product);
                return Ok(ResponseFactory.WithSuccess("Product Added!"));
            }
            catch (Exception e)
            {
                return BadRequest(ResponseFactory.WithError(e));
            }
        }
    }
}
