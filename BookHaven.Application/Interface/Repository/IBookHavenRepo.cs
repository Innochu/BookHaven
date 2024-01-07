using BookHaven.Domain.Entities;

namespace BookHaven.Application.Interface.Repository
{
    public interface IBookHavenRepo
    {
        IQueryable<Book> GetBooks();

        public Task<Book> GetBookByIdAsync(long Id);

        Task<IEnumerable<Book>> GetAllAsync();

       
    }
}
