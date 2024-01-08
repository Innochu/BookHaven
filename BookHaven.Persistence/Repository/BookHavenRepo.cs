using BookHaven.Application.Interface.Repository;
using BookHaven.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookHaven.Persistence.Repository
{
    public class BookHavenRepo : IBookHavenRepo
    {
        private readonly BookHavenDbContext _bookHavenDbContext;

        public BookHavenRepo(BookHavenDbContext bookHavenDbContext)
        {
            _bookHavenDbContext = bookHavenDbContext ?? throw new ArgumentNullException(nameof(BookHavenDbContext));
        }

        public IQueryable<Book> GetBooks()
        {
            return _bookHavenDbContext.Books.AsQueryable();
        }

        public async Task<Book> GetBookByIdAsync(string Id)
        {
            var getId = await _bookHavenDbContext.Books.FirstOrDefaultAsync(item => item.Id == Id);

            return getId;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _bookHavenDbContext.Books.Include(x => x.Category).ToListAsync();

        }

        public async Task<Book> CreateAsync(Book book)
        {
            await _bookHavenDbContext.Books.AddAsync(book);
            await _bookHavenDbContext.SaveChangesAsync();
            return book;

        }
    }
}

