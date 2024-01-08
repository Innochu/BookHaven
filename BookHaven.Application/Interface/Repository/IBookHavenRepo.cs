using BookHaven.Domain.Entities;

namespace BookHaven.Application.Interface.Repository
{
    public interface IBookHavenRepo
    {
        IQueryable<Book> GetBooks();

        Task<Book> GetBookByIdAsync(string Id);

        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book> CreateAsync(Book book);


    }
}
