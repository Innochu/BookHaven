namespace BookHaven.Application.Dto.ResponseDto
{
    public class OrderResponseDto
    {
        public long Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public int BookQuantity { get; set; }
        public decimal BookPrice { get; set; }
        public List<OrderItemResponseDto> OrderItems { get; set; } = new List<OrderItemResponseDto>();
        public decimal TotalPrice
        {
            get { return BookQuantity * BookPrice; }
        }
       

    }
}
