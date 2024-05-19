using Asp.Versioning;
using LR7.V3.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LR7.V3.Controllers.GenerateMethod {
    [ApiVersion("3.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class GenerateMethodController : ControllerBase {
        private readonly IGenerateMethodService _service;
        public GenerateMethodController(IGenerateMethodService service) {
            _service = service;
        }

        [MapToApiVersion("3.0")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GenerateExcel() {
            try {
                byte[] fileBytes = _service.GenerateExcelFile();
                string contentType = "application/octet-stream";
                return File(fileBytes, contentType, "v3API_GeneratedExcelFile.xlsx");
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
