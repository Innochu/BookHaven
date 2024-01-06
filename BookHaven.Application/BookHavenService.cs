using BookHaven.Application.Interface;
using BookHaven.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookHaven.Application
{
    public class BookHavenService : IBookHavenService
    {
        private readonly IBookHavenRepo _bookHavenRepository;
        private readonly ILogger<BookHavenService> _logger;

        public BookHavenService(IBookHavenRepo bookHavenRepository, ILogger<BookHavenService> logger)
        {
            _bookHavenRepository = bookHavenRepository ?? throw new ArgumentNullException(nameof(bookHavenRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<Book>> GetBooksAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            try
            {
                var getall = _bookHavenRepository.GetBooks();

                // Filtering
                if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
                {
                    switch (filterOn.ToLowerInvariant())
                    {
                        case "Author":
                            getall = getall.Where(book => EF.Functions.Like(book.Author, "%" + filterQuery + "%"));
                            break;
                        case "Title":
                            getall = getall.Where(book => EF.Functions.Like(book.Title, "%" + filterQuery + "%"));
                            break;
                    }
                }

                // Sorting
                if (!string.IsNullOrWhiteSpace(sortBy))
                {
                    if (sortBy.Equals("Title", StringComparison.OrdinalIgnoreCase))
                    {
                        getall = isAscending ? getall.OrderBy(item => item.Title) : getall.OrderByDescending(item => item.Title);
                    }
                }

                // Pagination
                var skipResult = (pageNumber - 1) * pageSize;
                var result = await getall.Skip(skipResult).Take(pageSize).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching books.");
                throw; // Rethrow the exception after logging
            }
        }

        public async Task<Book> GetBookByIdAsync(long id)
        {
            try
            {
                return await _bookHavenRepository.GetBookByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while fetching book with Id: {id}");
                throw; // Rethrow the exception after logging
            }
        }


    }
}

