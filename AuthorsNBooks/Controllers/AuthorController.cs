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
    public class AuthorController : Controller, IController<Author>
    {
        private readonly ApplicationContext context;

        public AuthorController(ApplicationContext context)
        {
            this.context = context;
        }

        [HttpPost("create")]
        public async Task<ActionResult> AddAsync(Author entity)
        {
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteByIdAsync(int id)
        {
            var author = context.Authors.Where(a => a.Id == id).FirstOrDefault();

            if (author is null) return BadRequest();

            context.Remove(author);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Author>>> GetAllAsync()
        {
            return Ok(await context.Authors.Include(a => a.Books).ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetByIdAsync(int id)
        {
            return Ok(await context.Authors.Where(a => a.Id == id).Include(a => a.Books).FirstOrDefaultAsync());
        }

        [HttpGet("{id}/count")]
        public async Task<ActionResult<int>> GetBookCount(int id)
        {
            return Ok(await context.Authors.Where(a => a.Id == id).Select(a => a.Books).CountAsync());
        }

        [HttpPut("{id}/addBook/{bookId}")]
        public async Task<ActionResult> UpdateAsync(int id, int bookId)
        {
            Author author = await context.Authors.Where(a => a.Id == id).Include(a => a.Books).FirstOrDefaultAsync();

            if (author is null) return BadRequest();

            Book book = await context.Books.Where(b => b.Id == bookId).FirstOrDefaultAsync();

            if (book is null) return BadRequest();

            author.Books.Add(book);
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}