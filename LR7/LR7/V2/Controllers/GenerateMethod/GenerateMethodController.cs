using Asp.Versioning;
using LR7.V2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LR7.V2.Controllers.GenerateMethod {
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class GenerateMethodController : ControllerBase {
        private readonly IGenerateMethodService _service;
        public GenerateMethodController(IGenerateMethodService service) {
            _service = service;
        }

        [MapToApiVersion("2.0")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GenerateString() {
            try {
                string value = _service.GenerateRandomString();
                return Ok(value);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
