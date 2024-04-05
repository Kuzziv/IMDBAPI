using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using IMDBLib.DataBase;
using IMDBLib.Models.Views;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IMDBLib.Services.DAOServices;
using IMDBLib.Models.People;
using IMDBLib.DAO;

namespace IMDBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        // GET: api/<PersonController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonView>>> Get(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var paginatedPersons = await _personService.GetAllPersons(pageNumber, pageSize);
                return Ok(paginatedPersons);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "An error occurred while retrieving data.");
            }
        }

        // make a controler that adds a person to the database
        [HttpPost]
        public async Task<ActionResult<PersonView>> Post(CrewDAO crewDAO)
        {
            try
            {
                var newPerson = await _personService.AddPerson(crewDAO);
                if (newPerson == true)
                {
                    return Ok(newPerson);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "An error occurred while adding a person.");
            }
        }
        
    }
}
