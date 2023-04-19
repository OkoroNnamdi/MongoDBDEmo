using Microsoft.AspNetCore.Mvc;
using MongoDbDemo.Model;
using MongoDbDemo.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MongoDbDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _services;
        public ProductController(IProductServices services)
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
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            product.CategoryName = null;
            await   _services.Create(product);
            return Ok("Created Sucessfully");
        }

        // PUT api/Category/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string  id, [FromBody] Product newproduct)
        {
            newproduct.CategoryName = null;
            var product = await _services.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            await _services.Update(id,  product );
            return Ok("Updated Sucessfully");
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult > Delete(string  id)
        {
            var category = await _services.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            await _services.Delete(id);
            return Ok("Record Deleted Sucessfully");
        }
    }
}
