namespace BookHaven.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; } = string.Empty; // Foreign key to link order with a user
        public DateTime OrderDate { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
