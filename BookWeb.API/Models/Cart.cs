namespace BookWeb.API.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string? userName { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
