using System.ComponentModel.DataAnnotations;

namespace BookWeb.API.Models.Auth
{
    public class Login
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
