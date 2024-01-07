using BookHaven.Domain.Entities;

namespace BookHaven.Application.Interface
{
    public interface IBookHavenRepo
    {
        IQueryable<Book> GetBooks();

        public Task<Book> GetBookByIdAsync(long Id);

        Task<IEnumerable<Book>> GetAllAsync();
    }
}
