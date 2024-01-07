using BookHaven.Domain.Entities;

namespace BookHaven.Application.Interface.Service
{
    public interface IOrderService
    {
        Task<Order> PlaceOrderAsync(string userId, List<OrderItem> orderItems);
    }
}
