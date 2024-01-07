using BookHaven.Application.Interface.Implementation;
using BookHaven.Application.Interface.Repository;
using BookHaven.Application.Interface.Service;
using BookHaven.Application.Services;
using BookHaven.Messaging;
using BookHaven.Persistence.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookHaven.Persistence.Extension
{
    public static class DIServiceExtension
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IBookHavenRepo, BookHavenRepo>();
            services.AddScoped<IOrderRepo, OrderRepo>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IBookHavenService, BookHavenService>();
            services.AddDbContext<BookHavenDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            var rabbitMQService = new RabbitMQService();
            var bookPublisher = new BookPublisher(rabbitMQService);
            var inventoryUpdater = new InventoryUpdater(rabbitMQService);

            services.AddSingleton(rabbitMQService);
            services.AddSingleton(bookPublisher);
            services.AddSingleton(inventoryUpdater);

        }

      
    }
}