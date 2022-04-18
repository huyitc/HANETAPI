using HANNET.API.Contracts;
using HANNET.API.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace HANNET.API.Controllers
{
    [Route("partner.hanet.ai/[controller]")]
    [ApiController]
    public class PlaceController : ControllerBase
    {
        private readonly IPlaceRepository _iplaceRepository;

        public PlaceController(IPlaceRepository iplaceRepository)
        {
            _iplaceRepository = iplaceRepository;
        }

        [HttpGet("/getPlaces")]
        public async Task<IActionResult> getPlaces()
        {
            var places = await _iplaceRepository.GetAllPlace();
            return Ok(places);
        }

        [HttpGet("/getPlaceInFo/{PlaceId}")]
        public async Task<IActionResult> GetById(int PlaceId)
        {
            var place = await _iplaceRepository.GetById(PlaceId);
            if (place== null)
                return BadRequest("Cannot find product");
            return Ok(place);
        }


        [HttpPost("/addPlaces")]
        public async Task<IActionResult> addPlaces([FromForm] PlaceAddModels models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var createdPlace = await _iplaceRepository.CreatePlace(models);
                return CreatedAtAction(nameof(addPlaces), new { id = createdPlace.PlaceId }, createdPlace);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("/updatePlaces")]

        public async Task<IActionResult> Update([FromForm] PlaceUpdateModels models)
        {
            var affectedResult = await _iplaceRepository.UpdatePlace(models);
            if (affectedResult == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("/removePlaces/{PlaceId}")]
        public async Task<IActionResult> Delete(int PlaceId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _iplaceRepository.Delete(PlaceId);
            if (result == 0)
                return BadRequest();
            return Ok();
        }

    }
}
