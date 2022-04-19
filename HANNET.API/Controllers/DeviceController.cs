using HANNET.API.Contracts;
using HANNET.API.ViewModel;
using HANNET.Data.Context;
using HANNET.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HANNET.API.Controllers
{
    [Route("partner.hanet.ai/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceRepository _deviceRepository;

        public DeviceController(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }
        [HttpGet("getListDevice")]
        public async Task<IActionResult> getListDevice()
        {
            var devices = await _deviceRepository.GetAll();
            return Ok(devices);
        }

        [HttpGet("get-list-device-by-placeId/{PlaceId}")]
        public async Task<IActionResult> getListDeviceByPlaceId(int PlaceId)
        {
            var devices = await _deviceRepository.GetByPlaceID(PlaceId);
            if (devices == null)
            {
                return BadRequest("Device not found");
            }
            return Ok(devices);
        }

        [HttpPut("updateDevice")]
        public async Task<IActionResult> updateDevice([FromForm] DeviceUpdateModels models)
        {
            var affectedResult = await _deviceRepository.Update(models);
            if (affectedResult == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost("addDevice")]
        public async Task<IActionResult> addDevice([FromForm] DeviceAddModels models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var createdPlace = await _deviceRepository.CreateDevice(models);
                return CreatedAtAction(nameof(addDevice), new { id = createdPlace.DeviceId }, createdPlace);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("deleteDevice")]
        public async Task<IActionResult> deleteDevice(int DevideId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _deviceRepository.Delete(DevideId);
            if (result == 0)
                return BadRequest();
            return Ok();
        }
    }
}
