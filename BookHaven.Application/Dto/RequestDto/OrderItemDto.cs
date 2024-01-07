namespace BookHaven.Application.Dto.RequestDto
{

    public class OrderItemDto
    {
        public long BookId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

}

