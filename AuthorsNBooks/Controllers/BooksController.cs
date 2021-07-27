using AuthorsNBooks.Data;
using AuthorsNBooks.Data.Contracts;
using AuthorsNBooks.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorsNBooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : Controller, IController<Book>
    {
        private readonly ApplicationContext context;

        public BooksController(ApplicationContext context)
        {
            this.context = context;
        }

        [HttpPost("create")]
        public async Task<ActionResult> AddAsync(Book entity)
        {
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteByIdAsync(int id)
        {
            var book = context.Books.Where(a => a.Id == id).FirstOrDefault();

            if (book is null) return BadRequest();

            context.Remove(book);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllAsync()
        {
            return Ok(await context.Books.Include(b => b.Authors).ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetByIdAsync(int id)
        {
            return Ok(await context.Books.Where(b => b.Id == id).Include(b => b.Authors).ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync(int id, int secondId)
        {
            return Ok();
        }
    }
}