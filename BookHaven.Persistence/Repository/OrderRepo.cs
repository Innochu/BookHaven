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
            _bookHavenDbContext.Orders.Update(order);
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

        public void UpdateStock(string bookId)
        {
            // Query the database to get the book with the specified bookId
            var book = _bookHavenDbContext.Books.FirstOrDefault(b => b.Id == bookId);

            if (book != null)
            {
                // Example: Increment the stock quantity for the given bookId
                book.Quantity++;

                // Save changes to the database
                _bookHavenDbContext.SaveChanges();

                Console.WriteLine($"Stock updated for book with ID {bookId}. New stock quantity: {book.Quantity}");
            }
            else
            {
                Console.WriteLine($"Book with ID {bookId} not found in the database.");
            }
        }

        public async Task<Order> CreateAsync(Order order)
        {
            await _bookHavenDbContext.Orders.AddAsync(order);
            await _bookHavenDbContext.SaveChangesAsync();
            return order;

        }
    }
}
