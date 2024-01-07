namespace BookHaven.Application.Dto.ResponseDto
{
    public class OrderStatusResponseDto
    {
        public long Id { get; set; }

        public string OrderId { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public DateTime StatusDate { get; set; }
    }
}
