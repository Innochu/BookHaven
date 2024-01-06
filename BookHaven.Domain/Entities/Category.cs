using System.ComponentModel.DataAnnotations;

namespace BookHaven.Domain.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; } = string.Empty;
        [Required]
     
        public ICollection<Book> Book { get; set; } = new List<Book>();
    }
}
