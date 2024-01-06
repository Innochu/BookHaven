using BookHaven.Domain.Entities;

namespace BookHaven.Application.Interface
{
    public interface IBookHavenRepo
    {
        public Task<List<Book>> GetBooksAsync(string? filterOn, string? filterQuery,
            string? sortBy, bool isAscending,
            int pageNumber, int pageSize);
    }
}
