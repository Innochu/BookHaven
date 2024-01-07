namespace BookHaven.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public long BookId { get; set; } // Foreign key to link order item with a book
        public string BookTitle { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; } = decimal.Zero;
    }
}
