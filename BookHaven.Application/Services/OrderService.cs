﻿using AutoMapper;
using BookHaven.Application.Dto.RequestDto;
using BookHaven.Application.Interface.Repository;
using BookHaven.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace BookHaven.Application.Services
{
    public class OrderService
    {
        private readonly IOrderRepo _orderRepository;
        private readonly ILogger<BookHavenService> _logger;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepo orderRepository, ILogger<BookHavenService> logger, IMapper mapper)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        public async Task<OrderRequestDto> PlaceOrderAsync(string userId, List<OrderItemDto> orderItems)
        {
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                OrderItems = _mapper.Map<List<OrderItem>>(orderItems)
            };

            var addedOrder = await _orderRepository.AddOrderAsync(order);

            // Map the addedOrder to OrderDto before returning
            return _mapper.Map<OrderRequestDto>(addedOrder);
        }
    }
}
