using BookWeb.API.Data;
using BookWeb.API.Interfaces;
using BookWeb.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookWeb.API.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationContext _context;
        public OrderService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllByUserNameAsync(string userName)
        {
            return await _context.Orders
                                 .Include(order => order.Items)
                                 .Where(user => user.UserName == userName)
                                 .ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders.Include(order => order.Items)
                                        .FirstOrDefaultAsync(order => order.Id == id);
        }

        public async Task<Order> CreateOrderAsync(string userName)
        {
            var cart = await _context.Carts.Include(cart => cart.Items)
                                           .FirstOrDefaultAsync(user => user.userName == userName);
            var order = new Order();
            
            if (cart != null)
            {
                if (cart.Items.Count() > 0)
                {
                    order.UserName = userName;
                    order.TotalPrice = cart.Items.Sum(i => i.TotalPrice);

                    foreach (var item in cart.Items)
                    {
                        var orderItem = new OrderItem
                        {
                            Quantity = item.Quantity,
                            UnitPrice = item.UnitPrice,
                            TotalPrice = item.TotalPrice,
                            BookId = item.BookId,
                        };
                        order.Items.Add(orderItem);
                        _context.OrderItems.Add(orderItem);
                    }
                    _context.Orders.Add(order);
                    cart.Items.Clear();
                    _context.Carts.Update(cart);
                    await _context.SaveChangesAsync();
                } 
            }
            return order;
        }
    }
}
