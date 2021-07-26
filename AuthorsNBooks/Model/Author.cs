using AuthorsNBooks.Data;

namespace AuthorsNBooks.Model
{
    public class Author : IEntity
    {
        public Book Books { get; set; }
        public int Id { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    }
}