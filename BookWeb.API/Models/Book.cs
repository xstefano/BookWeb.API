using System.ComponentModel.DataAnnotations;

namespace BookWeb.API.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public string? ImagenURL { get; set; }
        public float UnitPrice { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now; 
    }
}
