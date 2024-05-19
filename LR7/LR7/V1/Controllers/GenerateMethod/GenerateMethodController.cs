using Asp.Versioning;
using LR7.V1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LR7.V1.Controllers.GenerateMethod {
    [ApiVersion("1.0")]
    [Obsolete("This version is deprecated")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class GenerateMethodController : ControllerBase {
        private readonly IGenerateMethodService _service;
        public GenerateMethodController(IGenerateMethodService service) {
            _service = service;
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetNumber() {
            try {
                int value = _service.GenerateRandomNumber();
                return Ok(value);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
