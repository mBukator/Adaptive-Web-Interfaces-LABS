using Asp.Versioning;
using LR7.Models;
using LR7.Models.Response;
using LR7.Services.ParkingSpaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LR7.V2.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ParkingSpaceController : ControllerBase {
        private readonly IParkingSpaceService _parkingSpaceService;

        public ParkingSpaceController(IParkingSpaceService parkingSpaceService) {
            _parkingSpaceService = parkingSpaceService;
        }

        [MapToApiVersion("2.0")]
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetParkingSpacesAsync() {
            try {
                var parkingSpaces = await _parkingSpaceService.GetParkingSpacesAsync();
                return Ok(value: new BaseResponse(parkingSpaces));
            } catch (Exception ex) {
                return BadRequest(new BaseResponse(ex.Message));
            }
        }

        [MapToApiVersion("2.0")]
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetParkingSpaceById(int id) {
            try {
                var parkingSpace = await _parkingSpaceService.GetParkingSpaceByIdAsync(id);
                if (parkingSpace == null) {
                    return NotFound();
                }
                return Ok(value: new BaseResponse(parkingSpace));
            } catch (Exception ex) {
                return BadRequest(new BaseResponse(ex.Message));
            }
        }

        [MapToApiVersion("2.0")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateParkingSpace([FromBody] ParkingSpace parkingSpace) {
            try {
                if (!ModelState.IsValid) {
                    return BadRequest(ModelState);
                }
                await _parkingSpaceService.CreateParkingSpaceAsync(parkingSpace);
                //return CreatedAtAction("GetParkingSpaceById", new { id = parkingSpace.Id }, parkingSpace);
                return Ok(new BaseResponse("Parking Space created successfully"));

            } catch (Exception ex) {
                return BadRequest(new BaseResponse(ex.Message));
            }
        }

        [MapToApiVersion("2.0")]
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateParkingSpaceAsync(int id, [FromBody] ParkingSpace parkingSpace) {
            try {
                if (id != parkingSpace.Id) {
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid) {
                    return BadRequest(ModelState);
                }

                await _parkingSpaceService.UpdateParkingSpaceAsync(parkingSpace);
                return Ok(new BaseResponse("Parking Space updated successfully"));
            } catch (Exception ex) {
                return BadRequest(new BaseResponse(ex.Message));
            }
        }

        [MapToApiVersion("2.0")]
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParkingSpaceAsync(int id) {
            try {
                await _parkingSpaceService.DeleteParkingSpaceAsync(id);
                return Ok(new BaseResponse("Parking Space deleted successfully"));
            } catch (Exception ex) {
                return BadRequest(new BaseResponse(ex.Message));
            }
        }
    }
}
