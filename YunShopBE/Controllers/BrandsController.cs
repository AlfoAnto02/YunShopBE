using Application.Abstractions;
using Application.Models.DTOs;
using Application.Models.Request;
using Application.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace YunShopBE.Controllers {
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BrandsController : ControllerBase {
        private readonly IBrandService _brandService;
        public BrandsController(IBrandService brandService) {
            _brandService = brandService;
        }

        [HttpGet]
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

        [HttpPost]
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

        [HttpDelete]
        public async Task<IActionResult> DeleteByIdAsync(DeleteBrandRequest request) {
            try
            {
                await _brandService.DeleteByIdAsync(request.BrandId);
                return Ok(ResponseFactory.WithSuccess("Brand Deleted!"));
            }
            catch (Exception e)
            {
                return BadRequest(ResponseFactory.WithError(e));
            }
        }
    }
}
