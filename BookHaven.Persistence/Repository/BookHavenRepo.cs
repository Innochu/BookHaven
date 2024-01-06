﻿using BookHaven.Application.Interface;
using BookHaven.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookHaven.Persistence.Repository
{
    public class BookHavenRepo : IBookHavenRepo
    {
        private readonly BookHavenDbContext _bookHavenDbContext;

        public BookHavenRepo(BookHavenDbContext bookHavenDbContext)
        {
            _bookHavenDbContext = bookHavenDbContext ?? throw new ArgumentNullException(nameof(BookHavenDbContext));
        }

        public IQueryable<Book> GetBooks()
        {
            return _bookHavenDbContext.Books.AsQueryable();
        }

        public async Task<Book> GetBookByIdAsync(long Id)
        {
            var getId = await _bookHavenDbContext.Books.FirstOrDefaultAsync(item => item.Id == Id);

            return getId;
        }

    }
}

