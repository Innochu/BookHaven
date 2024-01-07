using AutoMapper;
using BookHaven.Application.Dto.ResponseDto;
using BookHaven.Application.Interface.Implementation;
using BookHaven.Application.Interface.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookHaven.Application.Services
{
    public class BookHavenService : IBookHavenService
    {
        private readonly IBookHavenRepo _bookHavenRepository;
        private readonly ILogger<BookHavenService> _logger;
        private readonly IMapper _mapper;

        public BookHavenService(IBookHavenRepo bookHavenRepository, ILogger<BookHavenService> logger, IMapper mapper)
        {
            _bookHavenRepository = bookHavenRepository ?? throw new ArgumentNullException(nameof(bookHavenRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        public async Task<List<BookHavenResponseDto>> GetBooksAsync(string? filterOn = null, string? filterQuery = null,
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

                if (result == null || result.Count == 0)
                {
                    throw new InvalidOperationException("No books found matching the criteria.");
                }

                // Map Book entities to BookHavenResponseDto
                var mappedResult = _mapper.Map<List<BookHavenResponseDto>>(result);

                return mappedResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching books.");
                throw; // Rethrow the exception after logging
            }
        }


        public async Task<BookHavenResponseDto> GetBookByIdAsync(long id)
        {
            try
            {
                var getbook = await _bookHavenRepository.GetBookByIdAsync(id);

                return _mapper.Map<BookHavenResponseDto>(getbook);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while fetching book with Id: {id}");
                throw; // Rethrow the exception after logging
            }
        }


        public async Task<List<BookHavenResponseDto>> GetAllBooksAsync()
        {
            try
            {
                var books = await _bookHavenRepository.GetAllAsync();

                if (books.Any())
                {
                    return _mapper.Map<List<BookHavenResponseDto>>(books);
                }
                else
                {
                    _logger.LogInformation("No books found.");

                    return new List<BookHavenResponseDto>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all books.");

                throw;
            }

        }




    }
}


