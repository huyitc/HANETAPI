using HANNET.API.Contracts;
using HANNET.API.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HANNET.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;

        public PersonController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        [HttpGet("{PersonId}")]
        public async Task<IActionResult> GetById(int PersonId)
        {
            var person = await _personRepository.GetById(PersonId);
            if (person == null)
                return BadRequest("Cannot find product");
            return Ok(person);
        }

        [HttpPost("/personRegister")]
        public async Task<IActionResult> Create([FromForm] PersonRegisterModels models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var PersonId = await _personRepository.Register(models);
            if (PersonId == 0)
                return BadRequest();

            var registerPerson = await _personRepository.GetById(PersonId);

            return CreatedAtAction(nameof(GetById), new { id = PersonId }, registerPerson);
        }
    }
}
