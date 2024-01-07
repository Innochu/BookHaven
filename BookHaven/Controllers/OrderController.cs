using AutoMapper;
using BookHaven.Application.Dto.RequestDto;
using BookHaven.Application.Dto.ResponseDto;
using BookHaven.Application.Interface.Service;
using Microsoft.AspNetCore.Mvc;

namespace BookHaven.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderService orderService, IMapper mapper, ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Places a new order.
        /// </summary>
        /// <param name="orderRequestDto">Order details.</param>
        /// <returns>Details of the placed order.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderRequestDto orderRequestDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var userId = "sampleUserId"; // You may get the user ID from your authentication system
                var orderItems = _mapper.Map<List<OrderItemDto>>(orderRequestDto.OrderItems);

                var placedOrder = await _orderService.PlaceOrderAsync(userId, orderItems);

                var orderResponseDto = _mapper.Map<OrderResponseDto>(placedOrder);

                return CreatedAtAction(nameof(GetOrderById), new { id = orderResponseDto.Id }, orderResponseDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }


        /// <summary>
        /// Gets details of a specific order by ID.
        /// </summary>
        /// <param name="id">Order ID.</param>
        /// <returns>Details of the order.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrderById(string id)
        {
            try
            {
                var orderDto = await _orderService.GetOrderByIdAsync(id);

                if (orderDto == null)
                {
                    return NotFound(); // Order not found
                }

                return Ok(orderDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while fetching order with Id: {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
    }

}


