using Application.Abstractions;
using Application.Models.DTOs;
using Application.Models.Request;
using Application.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace YunShopBE.Controllers {
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        public readonly IUserService _userService;
        public readonly ITokenService _tokenService;

        public UserController(IUserService userService, ITokenService _token)
        {
            _userService = userService;
            _tokenService = _token;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AddUserRequest userRequest)
        {
            try {
                var user = userRequest.ToEntity();
                await _userService.AddAsync(user);
                return Ok(ResponseFactory.WithSuccess("User Registered!"));
            }
            catch (Exception e) {
                return BadRequest(ResponseFactory.WithError(e));
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest){
        try {
            var user = await _userService.VerifyUserAsync(loginRequest);
            var tokenRequest = new CreateTokenRequest {
                userId = user.Id.ToString(),
                userName = user.Username,
                email = user.Email
            };
            var token = await _tokenService.CreateToken(tokenRequest);
            var userResponse = new UserResponse() {
                User = new UserDTO(user),
                Token = token
            };
            return Ok(ResponseFactory.WithSuccess(userResponse));
        }
        catch (Exception e) {
            return BadRequest(ResponseFactory.WithError(e));
        }
        }

        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try {
                var user = await _userService.GetAsync(id);
                return Ok(ResponseFactory.WithSuccess(new UserResponse() {
                    User = new UserDTO(user)
                }));
            }
            catch (Exception e) {
                return BadRequest(ResponseFactory.WithError(e));
            }
        }
    }
}
