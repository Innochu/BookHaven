using BookHaven.Domain.Entities;

namespace BookHaven.Application.Interface.Repository
{
    public interface IOrderRepo
    {
        Task<Order> AddOrderAsync(Order order);
        Task<Order> GetOrderByIdAsync(string Id);
    }
}
