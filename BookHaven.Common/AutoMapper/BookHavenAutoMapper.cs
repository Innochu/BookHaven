using AutoMapper;
using BookHaven.Application.Dto.RequestDto;
using BookHaven.Application.Dto.ResponseDto;
using BookHaven.Domain.Entities;

namespace BookHaven.AutoMapper
{
    public class BookHavenAutoMapper : Profile
    {
        public BookHavenAutoMapper()
        {
            CreateMap<BookHavenResponseDto, Book>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<Book, BookHavenRequestDto>().ReverseMap();


        }
    }
}
