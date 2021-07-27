using AuthorsNBooks.Data.Contracts;
using System.Collections.Generic;

namespace AuthorsNBooks.Model
{
    public class Book : IEntity
    {
        public List<Author> Authors { get; set; } = new();
        public int Id { get; set; }
        public string Name { get; set; }
    }
}