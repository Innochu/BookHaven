namespace BookHaven.Application.Dto.ResponseDto
{
    public class OrderItemResponseDto
    {
        public long BookId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
