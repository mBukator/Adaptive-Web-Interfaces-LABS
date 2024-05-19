using Asp.Versioning;
using LR7.Models;
using LR7.Models.Auth;
using LR7.Models.Response;
using LR7.Services;
using LR7.Services.Auth;
using LR7.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LR7.V3.Controllers {
    [ApiVersion("3.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    
    public class UserController : ControllerBase {
        private readonly IUserService _userService;

        public UserController(IUserService userService) {
            _userService = userService;
        }

        [MapToApiVersion("3.0")]
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers() {
            try {
                var users = await _userService.GetUsersAsync();
                return Ok(new BaseResponse(users));
            } catch (Exception ex) {
                return BadRequest(new BaseResponse(ex.Message));
            }
        }

        [MapToApiVersion("3.0")]
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id) {
            try {
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null) {
                    return NotFound();
                }
                return Ok(new BaseResponse(user));
            } catch (Exception ex) {
                return BadRequest(new BaseResponse(ex.Message));
            }
        }

        [MapToApiVersion("3.0")]
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user) {
            try {
                if (id != user.Id) {
                    return BadRequest();
                }

                if (!ModelState.IsValid) {
                    return BadRequest(ModelState);
                }

                var existingUser = await _userService.GetUserByIdAsync(user.Id);
                if (existingUser != null) {
                    existingUser.FirstName = user.FirstName;
                    existingUser.LastName = user.LastName;
                    existingUser.Email = user.Email;
                    existingUser.BirthDate = user.BirthDate;
                    existingUser.PhoneNumber = user.PhoneNumber;

                    await _userService.UpdateUserAsync(existingUser);

                    return Ok(new BaseResponse("User updated successfully"));
                } else {
                    return NotFound("User not found");
                }
            } catch (Exception ex) {
                return BadRequest(error: new BaseResponse(ex.Message));
            }
        }

        [MapToApiVersion("3.0")]
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id) {
            try {
                await _userService.DeleteUserAsync(id);
                return NoContent();
            } catch (Exception ex) {
                return BadRequest(error: new BaseResponse(ex.Message));
            }
        }
    }
}
