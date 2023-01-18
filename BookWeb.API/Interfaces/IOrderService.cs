using BookWeb.API.Models;

namespace BookWeb.API.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllByUserIdAsync(string userId);
        Task<Order?> GetByIdAsync(int id);
        Task<Order> CreateOrderAsync(string userId);
    }
}
