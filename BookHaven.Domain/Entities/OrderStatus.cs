namespace BookHaven.Domain.Entities
{
    public class OrderStatus
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string OrderId { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public DateTime StatusDate { get; set; } = DateTime.Now;
    }
}
