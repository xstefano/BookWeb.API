namespace BookWeb.API.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }
        public float TotalPrice { get; set; }
        public int BookId { get; set; }
        public Book? Book { get; set; }
    }
}
