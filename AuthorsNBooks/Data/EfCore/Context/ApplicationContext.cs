using AuthorsNBooks.Model;
using Microsoft.EntityFrameworkCore;

namespace AuthorsNBooks.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
            => Database.EnsureCreated();
    }
}