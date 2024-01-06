using BookHaven.Domain.Entities;

namespace BookHaven.Application.Interface
{
    public interface IBookHavenService
    {
        Task<List<Book>> GetBooksAsync(string? filterOn = null, string? filterQuery = null,
       string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000);
    }
}
