using BookWeb.API.Models;

namespace BookWeb.API.Interfaces
{
    public interface ICartService
    {
        Task<IEnumerable<Cart>> GetAllAsync();
        Task<Cart?> GetByUserIdAsync(string userId);
        Task<Cart?> AddItemAsync(string userId, int bookId);
        Task<Cart?> RemoveItemAsync(int cartId, int cartItemId);
        Task<Cart?> ClearCartAsync(string userId);
    }
}
