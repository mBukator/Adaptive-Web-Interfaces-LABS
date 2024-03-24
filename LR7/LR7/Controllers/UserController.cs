using LR7.Models;
using LR7.Models.Response;
using LR7.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace LR7.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase {

        private readonly IUserService _userService;

        public UserController(IUserService userService) {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersAsync() {
            try {
                var users = await _userService.GetUsersAsync();
                return Ok(new BaseResponse(users));
            } catch (Exception ex) {
                return BadRequest(new BaseResponse(ex.Message));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync(int id) {
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

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] User user) {
            try {
                if (!ModelState.IsValid) {
                    return BadRequest(ModelState);
                }

                await _userService.CreateUserAsync(user);

                //return CreatedAtAction("GetUserById", new { id = user.Id }, user);
                return Ok(new BaseResponse("User created successfully"));
            } catch (Exception ex) {
                return BadRequest(error: new BaseResponse(ex.Message));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] User user) {
            try {
                if (id != user.Id) {
                    return BadRequest();
                }

                if (!ModelState.IsValid) {
                    return BadRequest(ModelState);
                }

                await _userService.UpdateUserAsync(user);

                return Ok(new BaseResponse("User updated successfully"));
            } catch (Exception ex) {
                return BadRequest(error: new BaseResponse(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id) {
            try {
                await _userService.DeleteUserAsync(id);
                return Ok(new BaseResponse("User deleted successfully"));
            } catch (Exception ex) {
                return BadRequest(error: new BaseResponse(ex.Message));
            }
        }
    }
}
