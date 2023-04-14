using Microsoft.AspNetCore.Mvc;
using MongoDbDemo.Model;
using MongoDbDemo.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MongoDbDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices _services;
        public CategoryController(ICategoryServices services)
        {
            _services = services;
        }
        // GET: api/Category
        [HttpGet]
        public async Task <IActionResult > GetAll()
        {
           var data = await _services.GetAllAsync();
            return Ok(data);
        }

        // GET api/Category/5
        [HttpGet("{id}")]
        public async Task <IActionResult > GetByid(string  id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            var result = await _services.GetById(id);
            if (result == null)
            {
                return NotFound (result);
            }
            return Ok(result);
        }

        // POST api/Category
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Category category)
        {
            await   _services.Create(category);
            return Ok("Created Sucessfully");
        }

        // PUT api/Category/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
