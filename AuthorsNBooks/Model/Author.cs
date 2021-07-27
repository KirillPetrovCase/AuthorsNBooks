using AuthorsNBooks.Data.Contracts;
using System.Collections.Generic;

namespace AuthorsNBooks.Model
{
    public class Author : IEntity
    {
        public List<Book> Books { get; set; } = new();
        public int Id { get; set; }
        public string Name { get; set; }
    }
}