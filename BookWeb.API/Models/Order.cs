namespace BookWeb.API.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public float TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
