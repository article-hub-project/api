using BLL.Entities;
using DAL.Context;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MongoDBContext _context;
        public UsersController(MongoDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return Ok(await _context.Users.Find(_ => true).ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create([FromBody] User product)
        {
            await _context.Users.InsertOneAsync(product);
            return CreatedAtRoute(new { id = product.Id }, product);
        }
    }
}
