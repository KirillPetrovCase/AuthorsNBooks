using System.Collections.Generic;

namespace AuthorsNBooks.ViewModels

{
    public class BookVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Authors { get; set; }
    }
}