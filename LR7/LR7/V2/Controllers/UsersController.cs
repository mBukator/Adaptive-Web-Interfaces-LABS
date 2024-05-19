using Asp.Versioning;
using LR7.Models;
using LR7.Services.Users;
using LR7.Services.PasswordHash;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LR7.Context.Database;
using Serilog;

namespace LR7.V2.Controllers {
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase {
        private readonly IUserService _userService;

        public UsersController(IUserService userService) {
            _userService = userService;
        }

        // [ READ ]

        [MapToApiVersion("2.0")]
        [HttpGet]
        [Route("/get-all-users")]
        [Authorize]
        public async Task<IActionResult> GetAllUsersAsync() {
            var users = await _userService.GetUsersAsync();

            Log.Information("Users: {@users}", users);
            return Ok(users);
        }

        [MapToApiVersion("2.0")]
        [HttpGet]
        [Route("/get-user-by-id/{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserByIdAsync(int id) {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) {
                return NotFound();
            }

            Log.Debug("User By Id: {@user}", user);
            return Ok(user);
        }

        // [ CREATE ]

        [MapToApiVersion("2.0")]
        [HttpPost]
        [Route("/create-user")]
        [Authorize]
        public async Task<IActionResult> CreateUserAsync([FromBody] User user) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var createdUser = await _userService.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetUserByIdAsync), new { id = createdUser.Id }, createdUser);
        }

        // [ UPDATE ]

        [MapToApiVersion("2.0")]
        [HttpPut]
        [Route("/update-user/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] User user) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            if (id != user.Id) {
                return BadRequest();
            }

            await _userService.UpdateUserAsync(user);
            return NoContent();
        }

        // [ DELETE ]

        [MapToApiVersion("2.0")]
        [HttpDelete]
        [Route("/delete-user/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUserAsync(int id) {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
