using AutoMapper;
using BookHaven.Application.Dto.RequestDto;
using BookHaven.Application.Dto.ResponseDto;
using BookHaven.Application.Interface.Implementation;
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
                if (books.Any())
                {
                    return Ok(books);
                }
                else
                {
                    return NotFound(); 
                }
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
        public async Task<IActionResult> GetBookById(string id)
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }


        /// <summary>
        /// Gets all books.
        /// </summary>
        /// <returns>List of books.</returns>
        [HttpGet("GetAll-All-book")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                var books = await _bookHavenService.GetBooksAsync();

                if (books.Any())
                {
                    return Ok(books);
                }
                else
                {
                    _logger.LogInformation("No books found.");
                    return NotFound(); 
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }


        /// <summary>
        /// Creates a new book in the BookHaven system.
        /// </summary>
        /// <param name="bookRequestDto">The details of the book to be created.</param>
        /// <returns>Returns the created book details.</returns>
        /// <response code="201">Returns the newly created book.</response>
        /// <response code="400">If the request is invalid or malformed.</response>
        /// <response code="500">If an error occurs while processing the request.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        
        public async Task<ActionResult<BookHavenResponseDto>> CreateAsync([FromBody] BookHavenRequestDto bookRequestDto)
        {
            try
            {
                var createdBook = await _bookHavenService.CreateAsync(bookRequestDto);

                // Return 201 Created status along with the created book
                return CreatedAtAction(nameof(GetBookById), createdBook);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, $"Error creating book: {ex.Message}");

                // Return 500 Internal Server Error status along with the error message
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

    }
}

