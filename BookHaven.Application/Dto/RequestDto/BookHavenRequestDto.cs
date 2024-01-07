namespace BookHaven.Application.Dto.RequestDto
{
    public class BookHavenRequestDto
    {
        public long Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string ShortDescription { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public ICollection<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
    }
}
