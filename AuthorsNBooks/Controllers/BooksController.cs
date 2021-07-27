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
    public class BooksController : Controller
    {
        private readonly ApplicationContext context;

        public BooksController(ApplicationContext context)
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
            var book = context.Books.Where(a => a.Id == id)
                                    .FirstOrDefault();

            if (book is null) return BadRequest();

            context.Remove(book);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<BookVM>>> GetAllAsync()
        {
            var books = await context.Books.Include(b => b.Authors)
                                           .ToListAsync();

            List<BookVM> bookList = new();
            foreach (var book in books)
            {
                List<string> authors = new();
                foreach (var author in book.Authors)
                {
                    authors.Add(author.Name);
                }

                bookList.Add(new BookVM { Id = book.Id, Name = book.Name, Authors = authors });
            }

            return Ok(bookList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookVM>> GetByIdAsync(int id)
        {
            var book = await context.Books.Where(b => b.Id == id)
                                           .Include(b => b.Authors)
                                           .FirstOrDefaultAsync();

            List<string> authors = new();
            foreach (var author in book.Authors)
            {
                authors.Add(author.Name);
            }

            return Ok(new BookVM { Id = book.Id, Name = book.Name, Authors = authors });
        }

        [HttpPut("{id}/addAuthor/{authorId}")]
        public async Task<ActionResult> UpdateAsync(int id, int authorId)
        {
            Book books = await context.Books.Where(b => b.Id == id)
                                            .Include(b => b.Authors)
                                            .FirstOrDefaultAsync();

            if (books is null) return BadRequest();

            Author author = await context.Authors.Where(a => a.Id == authorId)
                                                 .FirstOrDefaultAsync();

            if (author is null) return BadRequest();

            books.Authors.Add(author);
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}