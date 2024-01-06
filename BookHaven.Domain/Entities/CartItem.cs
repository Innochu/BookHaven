namespace BookHaven.Domain.Entities
{
    public class CartItem
    {
        public long BookId { get; set; }
        public long UserId { get; set; } 
        public string BookTitle { get; set; } = string.Empty;
        public int BookQuantity { get; set; } 
        public decimal BookPrice { get; set; }
        public decimal Total
        {
            get { return BookQuantity * BookPrice; }
        }
        public string BookImage { get; set; } = string.Empty;

        public CartItem(Book book)
            {
            BookId = book.Id;
            BookTitle = book.Title;
            BookQuantity = 1;
            BookPrice = book.Price;
          
            }



    }
}
