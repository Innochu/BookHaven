using AutoMapper;
using BookHaven.Application.Dto;
using BookHaven.Application.Dto.ResponseDto;
using BookHaven.Application.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookHaven.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookHavenController : ControllerBase
    {
        private readonly IBookHavenService _bookHavenService;
        private readonly IMapper _mapper;
        private readonly ILogger<BookHavenController> _logger;

        public BookHavenController(IBookHavenService bookHavenService, IMapper mapper, ILogger<BookHavenController> logger)
        {
            _bookHavenService = bookHavenService;
            _mapper = mapper;
            _logger = logger;
        }


        /// <summary>
        /// Gets all books.
        /// </summary>
        /// <param name="filterOn">Filter criteria.</param>
        /// <param name="filterQuery">Filter query.</param>
        /// <param name="sortBy">Sort by criteria.</param>
        /// <param name="isAscending">Sort order.</param>
        /// <param name="pageNumber">Page number.</param>
        /// <param name="pageSize">Page size.</param>
        /// <returns>List of books.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
        [FromQuery] string? sortBy, [FromQuery] bool isAscending,
        [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            try
            {
                var books = await _bookHavenService.GetBooksAsync(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }


        /// <summary>
        /// Gets a book by its ID.
        /// </summary>
        /// <param name="id">Book ID.</param>
        /// <returns>The requested book.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBookById(long id)
        {
            try
            {
                var bookDto = await _bookHavenService.GetBookByIdAsync(id);
                if (bookDto == null)
                {
                    return NotFound(); // Book not found
                }
                return Ok(bookDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while fetching book with Id: {id}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
