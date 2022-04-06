using HANNET.API.Contracts;
using HANNET.API.ViewModel;
using HANNET.Data.Context;
using HANNET.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HANNET.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceRepository _deviceRepository;

        public DeviceController(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }
        [HttpGet]
        [Route("getListDevice")]

        public async Task<IActionResult> getListDevice()
        {
            var devices = await _deviceRepository.GetAll();
            return Ok(devices);
        }

        [HttpGet("{PlaceId}")]
        public async Task<IActionResult> getListDeviceByPlaceId(int PlaceId)
        {
            var devices = await _deviceRepository.GetByPlaceID(PlaceId);
            if(devices==null)
            {
                return NotFound("Device not found");
            } 
            return Ok(devices);
        }
        
        [HttpPut]
        public async Task<IActionResult> updateDevice(DeviceUpdateModels models)
        {
           var affectedResult = await _deviceRepository.Update(models);
            if (affectedResult == 0)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
