using BookHaven.Application.Dto.RequestDto;

namespace BookHaven.Application.Dto.ResponseDto
{
    public class BookHavenResponseDto
    {
        public long Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string ShortDescription { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public ICollection<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
    }
}
