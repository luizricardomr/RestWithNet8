using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RestWithNet8.Api.Business;
using RestWithNet8.Api.Model;

namespace RestWithNet8.Api.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class BookController : ControllerBase
    {

        private readonly ILogger<BookController> _logger;
        private readonly IBookBusiness _bookBusiness;

        public BookController(ILogger<BookController> logger, IBookBusiness bookBusiness)
        {
            _logger = logger;
            _bookBusiness = bookBusiness;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bookBusiness.FinAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var person = _bookBusiness.FindById(id);
            if (person == null) return NotFound();

            return Ok(person);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Book person)
        {            
            if (person == null) return BadRequest();

            return Ok(_bookBusiness.Create(person));
        }

        [HttpPut]
        public IActionResult Put([FromBody] Book person)
        {
            if (person == null) return BadRequest();

            return Ok(_bookBusiness.Update(person));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _bookBusiness.Delete(id);            
            return NoContent();
        }
    }
}
