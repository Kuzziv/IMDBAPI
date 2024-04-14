// PersonController.cs
using IMDBLib.DTO;
using IMDBLib.Models.Views;
using IMDBLib.Services.APIServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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

        [HttpGet("{nmconst}")]
        public async Task<IActionResult> GetPersonByNmconstAsync(string nmconst)
        {
            try
            {
                var person = await _personService.GetPersonByNmconstAsync(nmconst);
                if (person == null)
                    return NotFound($"Person with Nconst {nmconst} not found.");
                return Ok(person);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllPersonsAsync(int page = 1, int pageSize = 10)
        {
            try
            {
                var persons = await _personService.GetAllPersonsAsync(page, pageSize);
                return Ok(persons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddPersonAsync(PersonDTO person)
        {
            try
            {
                if (await _personService.AddPersonAsync(person) == true)
                {
                    return StatusCode(201, "Person added successfully.");
                }
                return StatusCode(400, "Some whent worng");

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchPersonsByNameAsync(string searchName, int page = 1, int pageSize = 10)
        {
            try
            {
                var persons = await _personService.SearchPersonsByNameAsync(searchName, page, pageSize);
                return Ok(persons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
