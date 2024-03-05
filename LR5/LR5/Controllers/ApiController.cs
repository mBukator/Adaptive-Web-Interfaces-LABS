using LR5.Models;
using LR5.Services;
using Microsoft.AspNetCore.Mvc;


namespace LR5.Controllers {
    [ApiController]
    [Route("api")]
    public class ApiController : ControllerBase {

        private readonly ApiClient _apiClient;

        public ApiController(ApiClient apiClient) {
            _apiClient = apiClient;
        }

        
        [HttpGet]
        public async Task<IActionResult> Get() {
            try {
                string data = await _apiClient.GetNYTPopularAsync();

                var responseModel = new ResponseModel<string> {
                    Message = "ALL IS GOOD!",
                    StatusCode = StatusCodes.Status200OK,
                    Data = new List<string> { data },
                };

                return Ok(responseModel);

            } catch (Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post() {
            try {
                string data = await _apiClient.PostNYTPopularAsync("viewed", 1);


                var responseModel = new ResponseModel<string> {
                    Message = "ALL IS GOOD!",
                    StatusCode = StatusCodes.Status200OK,
                    Data = new List<string> { data },
                };

                return Ok(responseModel);

            } catch (Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
