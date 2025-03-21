using Application.Abstractions;
using Application.Models.DTOs;
using Application.Models.Request;
using Application.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace YunShopBE.Controllers {
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BrandController : ControllerBase {
        private readonly IBrandService _brandService;
        public BrandController(IBrandService brandService) {
            _brandService = brandService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync() {
            try
            {
                var brands = await _brandService.GetAllAsync();
                var brandsDTOs = brands.Select(b => new BrandDTO(b)).ToList();
                return Ok(ResponseFactory.WithSuccess(brands));
            }
            catch (Exception e)
            {
                return BadRequest(ResponseFactory.WithError(e));
            }
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync([FromBody] AddBrandRequest addBrandRequest) {
            try
            {
                var brand = addBrandRequest.ToEntity();
                await _brandService.AddAsync(brand);
                return Ok(ResponseFactory.WithSuccess("Brand Added!"));
            }
            catch (Exception e)
            {
                return BadRequest(ResponseFactory.WithError(e));
            }
        }
    }
}
