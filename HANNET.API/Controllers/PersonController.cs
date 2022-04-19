using HANNET.API.Contracts;
using HANNET.API.ViewModel;
using HANNET.API.ViewModel.PersonImages;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HANNET.API.Controllers
{
    [Route("partner.hanet.ai/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;

        public PersonController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }


        [HttpGet("getlistByPlace/{PlaceId}")]
        public async Task<IActionResult> GetByPlaceId(int PlaceId)
        {
            var person = await _personRepository.GetByPlaceId(PlaceId);
            if (person == null)
                return BadRequest("Cannot find product");
            return Ok(person);
        }

        [HttpGet("getlistByAliasID/{AliasID}")]
        public async Task<IActionResult> GetByAliasId(int AliasID)
        {
            var person = await _personRepository.GetByAliasId(AliasID);
            if (person == null)
                return BadRequest("Cannot find person");
            return Ok(person);
        }

        [HttpPost("personRegister")]
        public async Task<IActionResult> personRegister([FromForm] PersonRegisterModels models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var createdPerson = await _personRepository.PersonRegister(models);
                return CreatedAtAction(nameof(personRegister), new { id = createdPerson }, createdPerson);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("personRegisterByUrl")]
        public async Task<IActionResult> personRegisterByUrl([FromForm] PersonRegisterByUrl models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var createdPerson = await _personRepository.PersonRegisterByUrl(models);
                return CreatedAtAction(nameof(personRegister), new { id = createdPerson }, createdPerson);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("updateByFaceImagePersionId/")]
        public async Task<IActionResult> UpdateImage(int PersonId, [FromForm] PersonImageUpdate personImages)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _personRepository.UpdateImage(PersonId, personImages);
            if (result == 0)
                return BadRequest();

            return Ok();
        }


        [HttpPut("updateByFaceUrlPersionId/")]
        public async Task<IActionResult> UpdateUrl(int PersonId, [FromForm] PersonUrlUpdate personUrls)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _personRepository.UpdateUrl(PersonId, personUrls);
            if (result == 0)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("removePersonByID/{PersonId}")]
        public async Task<IActionResult> DeletePerson(int PersonId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _personRepository.DeletePerson(PersonId);
            if (result == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("removePersonByAliasID/{AliasID}")]
        public async Task<IActionResult> DeletePersonByAliasID(int AliasID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _personRepository.DeletePersonByAliasID(AliasID);
            if (result == 0)
                return BadRequest();
            return Ok();
        }


        [HttpPut("updatePerson")]

        public async Task<IActionResult> Update([FromForm] PersonUpdateModels models)
        {
            var affectedResult = await _personRepository.updatePerson(models);
            if (affectedResult == 0)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
