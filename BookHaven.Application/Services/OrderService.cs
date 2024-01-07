using AutoMapper;
using BookHaven.Application.Dto.RequestDto;
using BookHaven.Application.Dto.ResponseDto;
using BookHaven.Application.Interface.Repository;
using BookHaven.Application.Interface.Service;
using BookHaven.Domain.Entities;
using BookHaven.Messaging;
using Microsoft.Extensions.Logging;

namespace BookHaven.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _orderRepository;
        private readonly ILogger<BookHavenService> _logger;
        private readonly IMapper _mapper;
        private readonly BookPublisher _bookPublisher; // New addition for RabbitMQ

        public OrderService(IOrderRepo orderRepository, ILogger<BookHavenService> logger, IMapper mapper, BookPublisher bookPublisher)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
            _bookPublisher = bookPublisher ?? throw new ArgumentNullException(nameof(bookPublisher));
        }

        public async Task<OrderRequestDto> PlaceOrderAsync(string userId, List<OrderItemDto> orderItems)
        {
            var order = new Order
            {
                OrderId = userId,
                OrderDate = DateTime.UtcNow,
                OrderItems = _mapper.Map<List<OrderItem>>(orderItems)
            };

            var addedOrder = await _orderRepository.AddOrderAsync(order);

            // Map the addedOrder to OrderDto before returning
            var orderDto = _mapper.Map<OrderRequestDto>(addedOrder);

            // Notify other components about the new order
            _bookPublisher.PublishNewOrder(orderDto.OrderId);

            return orderDto;
        }

      
    

    public async Task<OrderResponseDto> GetOrderByIdAsync(string id)
        {
            try
            {
                var getbook = await _orderRepository.GetOrderByIdAsync(id);

                return _mapper.Map<OrderResponseDto>(getbook);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while fetching book with Id: {id}");
                throw; // Rethrow the exception after logging
            }
        }

        public async Task<OrderStatusResponseDto> GetOrderStatusByIdAsync(string id)
        {
            // Logic to fetch the order status from the repository or any other source
            var orderStatus = await _orderRepository.GetOrderStatusByIdAsync(id);

            return _mapper.Map<OrderStatusResponseDto>(orderStatus);
        }

    }
}
