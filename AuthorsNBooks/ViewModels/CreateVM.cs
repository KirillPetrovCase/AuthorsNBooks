using System.ComponentModel.DataAnnotations;

namespace AuthorsNBooks.ViewModels
{
    public class CreateVM
    {
        [Required]
        public string Name { get; set; }
    }
}