using BookHaven.Application.Interface.Repository;
using BookHaven.Domain.Entities;

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
    }
}
