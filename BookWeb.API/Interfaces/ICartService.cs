using BookWeb.API.Models;

namespace BookWeb.API.Interfaces
{
    public interface ICartService
    {
        Task<IEnumerable<Cart>> GetAllAsync();
        Task<Cart?> GetByUserNameAsync(string userName);
        Task<Cart?> AddItemAsync(string userName, int bookId);
        Task<Cart?> RemoveItemAsync(int cartId, int cartItemId);
        Task<Cart?> ClearCartAsync(string userName);
    }
}
