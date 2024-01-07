using BookHaven.Application.Interface.Repository;
using BookHaven.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookHaven.Persistence.Repository
{
    public class OrderRepo : IOrderRepo
    {

        private readonly BookHavenDbContext _bookHavenDbContext;

        public OrderRepo(BookHavenDbContext bookHavenDbContext)
        {
            _bookHavenDbContext = bookHavenDbContext ?? throw new ArgumentNullException(nameof(BookHavenDbContext));
        }

        public async Task<Order> AddOrderAsync(Order order)
        {
            _bookHavenDbContext.Orders.Add(order);
            await _bookHavenDbContext.SaveChangesAsync();
            return order;
        }

        public async Task<Order> GetOrderByIdAsync(string Id)
        {
            var getId = await _bookHavenDbContext.Orders.FirstOrDefaultAsync(item => item.Id == Id);

            return getId;
        }

        public async Task<OrderStatus> GetOrderStatusByIdAsync(string id)
        {
            // Assuming there's a DbSet<OrderStatus> in your DbContext
            return await _bookHavenDbContext.OrderStatuses.FirstOrDefaultAsync(os => os.OrderId == id);
        }
    }
}
