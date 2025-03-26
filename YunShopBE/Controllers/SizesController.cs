using Application.Abstractions;
using Application.Models.DTOs;
using Application.Models.Request;
using Application.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace YunShopBE.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SizesController : ControllerBase{ 
        private readonly ISizeService _sizeService;
        public SizesController(ISizeService sizeService) {
            _sizeService = sizeService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync() {
            try {
                var sizes = await _sizeService.GetAllAsync();
                var sizeDtOs = sizes.Select(size => new SizeDTO(size)).ToList();
                return Ok(ResponseFactory.WithSuccess(sizes));
            }
            catch (Exception e) {
                return BadRequest(ResponseFactory.WithError(e));
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] AddSizeRequest addSizeRequest) {
            try {
                var size = addSizeRequest.ToEntity();
                await _sizeService.AddAsync(size);
                return Ok(ResponseFactory.WithSuccess("Size Added!"));
            }
            catch (Exception e) {
                return BadRequest(ResponseFactory.WithError(e));
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteByIdAsync(DeleteSizeRequest request) {
            try {
                await _sizeService.DeleteByIdAsync(request.SizeId);
                return Ok(ResponseFactory.WithSuccess("Size Deleted!"));
            }
            catch (Exception e) {
                return BadRequest(ResponseFactory.WithError(e));
            }
        }

    }
}
