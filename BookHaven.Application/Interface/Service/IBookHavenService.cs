using BookHaven.Application.Dto.ResponseDto;
using BookHaven.Domain.Entities;

namespace BookHaven.Application.Interface.Implementation
{
    public interface IBookHavenService
    {
        Task<List<BookHavenResponseDto>> GetBooksAsync(string? filterOn = null, string? filterQuery = null,
       string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000);

        Task<BookHavenResponseDto> GetBookByIdAsync(string id);
        Task<List<BookHavenResponseDto>> GetAllBooksAsync();
       
    }
}
