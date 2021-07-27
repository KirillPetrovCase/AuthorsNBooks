using AuthorsNBooks.Data;
using AuthorsNBooks.Model;
using AuthorsNBooks.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorsNBooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : Controller
    {
        private readonly ApplicationContext context;

        public AuthorController(ApplicationContext context)
        {
            this.context = context;
        }

        [HttpPost("create")]
        public async Task<ActionResult> AddAsync(CreateVM createViewModel)
        {
            await context.AddAsync(createViewModel.Name);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}/delete")]
        public async Task<ActionResult> DeleteByIdAsync(int id)
        {
            var author = context.Authors.Where(a => a.Id == id).FirstOrDefault();

            if (author is null) return BadRequest();

            context.Remove(author);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<AuthorVM>>> GetAllAsync()
        {
            var authorList = await context.Authors.Include(a => a.Books)
                                                  .ToListAsync();

            List<AuthorVM> authors = new();

            foreach (var author in authorList)
            {
                List<string> booksName = new();
                foreach (var book in author.Books)
                {
                    booksName.Add(book.Name);
                }

                authors.Add(new AuthorVM { Id = author.Id, Name = author.Name, Books = booksName });
            }

            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorVM>> GetByIdAsync(int id)
        {
            Author author = await context.Authors.Where(a => a.Id == id)
                                               .Include(a => a.Books)
                                               .FirstOrDefaultAsync();

            List<string> booksName = new();
            foreach (var book in author.Books)
            {
                booksName.Add(book.Name);
            }

            return Ok(new AuthorVM { Id = author.Id, Name = author.Name, Books = booksName});
        }

        [HttpGet("{id}/count")]
        public async Task<ActionResult<int>> GetBookCount(int id)
        {
            return Ok(await context.Authors.Where(a => a.Id == id)
                                           .Select(a => a.Books)
                                           .CountAsync());
        }

        [HttpPut("{id}/addBook/{bookId}")]
        public async Task<ActionResult> UpdateAsync(int id, int bookId)
        {
            Author author = await context.Authors.Where(a => a.Id == id)
                                                 .Include(a => a.Books)
                                                 .FirstOrDefaultAsync();

            if (author is null) return BadRequest();

            Book book = await context.Books.Where(b => b.Id == bookId)
                                           .FirstOrDefaultAsync();

            if (book is null) return BadRequest();

            author.Books.Add(book);
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}