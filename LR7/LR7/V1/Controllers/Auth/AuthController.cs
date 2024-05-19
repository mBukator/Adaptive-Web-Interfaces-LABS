using Asp.Versioning;
using LR7.Models;
using LR7.Models.Auth;
using LR7.Models.Response;
using LR7.Services;
using LR7.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LR7.V1.Controllers {
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Obsolete("This version is deprecated")]
    [ApiController]
    public class AuthController : ControllerBase {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) {
            _authService = authService;
        }

        [MapToApiVersion("1.0")]
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] AuthPayload authPayload) {
            try {
                var authUser = await _authService.LoginAsync(authPayload.Email, authPayload.Password);

                if (authUser == null) {
                    return Unauthorized("Invalid email or password. Please, try again ;(");
                }

                var token = _authService.GenerateToken(authUser);

                return Ok(token);
            } catch (Exception ex) {
                return BadRequest(new BaseResponse(ex.Message));
            }
        }

        [MapToApiVersion("1.0")]
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] User user) {
            try {
                var createdUser = await _authService.RegistrateAsync(user);
                if (createdUser == null) {
                    return BadRequest(new BaseResponse("Something went wrong while trying to registrate user ;("));
                }
                return Ok(createdUser);
            } catch (Exception ex) {
                return BadRequest(new BaseResponse(ex.Message));
            }
        }

    }
}
