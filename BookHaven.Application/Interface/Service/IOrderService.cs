using BookHaven.Application.Dto.RequestDto;
using BookHaven.Application.Dto.ResponseDto;
using BookHaven.Domain.Entities;

namespace BookHaven.Application.Interface.Service
{
    public interface IOrderService
    {
        Task<OrderRequestDto> PlaceOrderAsync(string userId, List<OrderItemDto> orderItems);
        Task<OrderResponseDto> GetOrderByIdAsync(string id);


    }
}
