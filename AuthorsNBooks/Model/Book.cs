using AuthorsNBooks.Data;

namespace AuthorsNBooks.Model
{
    public class Book : IEntity
    {
        public Author Authors { get; set; }
        public int Id { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    }
}