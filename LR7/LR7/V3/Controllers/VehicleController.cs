using Asp.Versioning;
using LR7.Models;
using LR7.Models.Response;
using LR7.Services.Vehicles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LR7.V3.Controllers
{
    [ApiVersion("3.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService) {
            _vehicleService = vehicleService;
        }

        [MapToApiVersion("3.0")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetVehiclesAsync() {
            try {
                var vehicles = await _vehicleService.GetVehiclesAsync();
                return Ok(value: new BaseResponse(vehicles));
            } catch (Exception ex) {
                return BadRequest(new BaseResponse(ex.Message));
            }
        }

        [MapToApiVersion("3.0")]
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetVehicleByIdAsync(int id) {
            try {
                var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
                if (vehicle == null) {
                    return BadRequest();
                }
                return Ok(value: new BaseResponse(vehicle));
            } catch (Exception ex) {
                return BadRequest(new BaseResponse(ex.Message));
            }
        }

        [MapToApiVersion("3.0")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateVehicleAsync([FromBody] Vehicle vehicle) {
            try {
                if (!ModelState.IsValid) {
                    return BadRequest();
                }
                await _vehicleService.CreateVehicleAsync(vehicle);
                return Ok(new BaseResponse("Vehicle created successfully"));
            } catch (Exception ex) {
                return BadRequest(new BaseResponse(ex.Message));
            }
        }

        [MapToApiVersion("3.0")]
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateVehicleAsync(int id, [FromBody] Vehicle vehicle) {
            try {
                if (id != vehicle.Id) {
                    return BadRequest();
                }
                if (!ModelState.IsValid) {
                    return BadRequest();
                }
                await _vehicleService.UpdateVehicleAsync(vehicle);
                return Ok(new BaseResponse("Vehicle updated successfully"));
            } catch (Exception ex) {
                return BadRequest(new BaseResponse(ex.Message));
            }
        }

        [MapToApiVersion("3.0")]
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteVehicleAsync(int id) {
            try {
                await _vehicleService.DeleteVehicleAsync(id);
                return Ok(new BaseResponse("Vehicle deleted successfully"));
            } catch (Exception ex) {
                return BadRequest(new BaseResponse(ex.Message));
            }
        }
    }
}
