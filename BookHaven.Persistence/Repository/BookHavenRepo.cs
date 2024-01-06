using BookHaven.Application.Interface;
using BookHaven.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookHaven.Persistence.Repository
{
    public class BookHavenRepo : IBookHavenRepo
    {
        private readonly BookHavenDbContext _bookHavenDbContext;

        public BookHavenRepo(BookHavenDbContext bookHavenDbContext)
        {
            _bookHavenDbContext = bookHavenDbContext;
        }
        public async Task<List<Book>> GetBooksAsync(string? filterOn = null, string? filterQuery = null,
              string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            //    var getall = _bookHavenDbContext. .AsQueryable();

            //    //filtering
            //    if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            //    {
            //        switch (filterOn.ToLowerInvariant())
            //        {
            //            case "name":
            //                getall = getall.Where(region => EF.Functions.Like(region.Name, "%" + filterQuery + "%"));
            //                break;
            //            case "code":
            //                getall = getall.Where(region => EF.Functions.Like(region.Code, "%" + filterQuery + "%"));
            //                break;
            //        }
            //    }

            //    //sorting

            //    if (string.IsNullOrWhiteSpace(sortBy) == false)
            //    {
            //        if (sortBy.Equals("name", StringComparison.OrdinalIgnoreCase))
            //        {
            //            getall = isAscending ? getall.OrderBy(item => item.Name) : getall.OrderByDescending(item => item.Name);
            //        }
            //    }

            //    //pagination
            //    var skipResult = (pageNumber - 1) * pageSize;

            //    return await getall.Skip(pageNumber).Take(pageSize).ToListAsync();
            return null;
        }

       
    }
}

