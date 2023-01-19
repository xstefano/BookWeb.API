using BookWeb.API.Models;

namespace BookWeb.API.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllByUserNameAsync(string userName);
        Task<Order?> GetByIdAsync(int id);
        Task<Order> CreateOrderAsync(string userName);
    }
}
