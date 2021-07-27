using System.Collections.Generic;

namespace AuthorsNBooks.ViewModels
{
    public class AuthorVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Books{ get; set; }
    }
}