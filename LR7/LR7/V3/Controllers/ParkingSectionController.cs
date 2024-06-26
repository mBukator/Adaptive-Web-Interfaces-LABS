﻿using Asp.Versioning;
using LR7.Models;
using LR7.Models.Response;
using LR7.Services.ParkingSections;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LR7.V3.Controllers
{
    [ApiVersion("3.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ParkingSectionController : ControllerBase {
        private readonly IParkingSectionService _parkingSectionService;

        public ParkingSectionController(IParkingSectionService parkingSectionService) {
            _parkingSectionService = parkingSectionService;
        }

        [MapToApiVersion("3.0")]
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetParkingSectionsAsync() {
            try {
                var parkingSections = await _parkingSectionService.GetParkingSectionsAsync();
                return Ok(value: new BaseResponse(parkingSections));
            } catch (Exception ex) {
                return BadRequest(new BaseResponse(ex.Message));
            }
        }

        [MapToApiVersion("3.0")]
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetParkingSectionByIdAsync(int id) {
            try {
                var parkingSection = await _parkingSectionService.GetParkingSectionByIdAsync(id);
                if (parkingSection == null) {
                    return NotFound();
                }
                return Ok(value: new BaseResponse(parkingSection));
            } catch (Exception ex) {
                return BadRequest(new BaseResponse(ex.Message));
            }
        }

        [MapToApiVersion("3.0")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateParkingSectionAsync([FromBody] ParkingSection parkingSection) {
            try {
                if (!ModelState.IsValid) {
                    return BadRequest(ModelState);
                }

                await _parkingSectionService.CreateParkingSectionAsync(parkingSection);
                //return CreatedAtAction("GetParkingSectionById", new { id = parkingSection.Id }, parkingSection);
                return Ok(new BaseResponse("Parking Section created successfully"));
            } catch (Exception ex) {
                return BadRequest(new BaseResponse(ex.Message));
            }
        }

        [MapToApiVersion("3.0")]
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateParkingSectionAsync(int id, [FromBody] ParkingSection parkingSection) {
            try {
                if (id != parkingSection.Id) {
                    return BadRequest();
                }
                if (!ModelState.IsValid) {
                    return BadRequest(ModelState);
                }

                await _parkingSectionService.UpdateParkingSectionAsync(parkingSection);
                return Ok(new BaseResponse("Parking Section updated successfully"));
            } catch (Exception ex) {
                return BadRequest(new BaseResponse(ex.Message));
            }
        }

        [MapToApiVersion("3.0")]
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParkingSectionAsync(int id) {
            try {
                await _parkingSectionService.DeleteParkingSectionAsync(id);
                return Ok(new BaseResponse("Parking Section deleted successfully"));
            } catch (Exception ex) {
                return BadRequest(new BaseResponse(ex.Message));
            }
        }
    }
}
