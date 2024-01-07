using BookHaven.Domain.Entities;

namespace BookHaven.Application.Dto.RequestDto
{
    public class OrderRequestDto
    {
        public string OrderId { get; set; } = string.Empty;
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
