using Application.Abstractions;
using Application.Models.Request;
using Application.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace YunShopBE.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SizeController : ControllerBase{ 
        private readonly ISizeService _sizeService;
        public SizeController(ISizeService sizeService) {
            _sizeService = sizeService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync() {
            try {
                var sizes = await _sizeService.GetAllAsync();
                return Ok(ResponseFactory.WithSuccess(sizes));
            }
            catch (Exception e) {
                return BadRequest(ResponseFactory.WithError(e));
            }
        }

        [HttpPost("Add")]
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

    }
}
