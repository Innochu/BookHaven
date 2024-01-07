using BookHaven.Application;
using BookHaven.Application.Interface;
using BookHaven.Persistence.Repository;
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
            services.AddScoped<IBookHavenService, BookHavenService>();
            services.AddDbContext<BookHavenDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

        }
    }
}