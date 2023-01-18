using BookWeb.API.Data;
using BookWeb.API.Interfaces;
using BookWeb.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookWeb.API.Services
{
    public class CartService : ICartService
    {
        private readonly ApplicationContext _context;
        public CartService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cart>> GetAllAsync()
        {
            return await _context.Carts.Include(cart => cart.Items)
                                       .ToListAsync();
        }

        public async Task<Cart?> GetByUserIdAsync(string userId)
        {
            return await _context.Carts.Include(cart => cart.Items)        
                                       .FirstOrDefaultAsync(user => user.UserId == userId);
        }

        public async Task<Cart?> AddItemAsync(string userId, int bookId)
        {
            var existingCart = await GetByUserIdAsync(userId);

            if (existingCart == null)
            {
                existingCart = await CreateNewCart(userId);

                if (existingCart == null) return null;
            }

            var book = await _context.Books.FindAsync(bookId);
            var item = existingCart.Items.FirstOrDefault(book => book.BookId == bookId);

            if (item != null)
            {
                item.Quantity++;
                item.TotalPrice = item.Quantity * item.UnitPrice;
            }
            else
            {
                existingCart.Items.Add(new CartItem()
                {
                    Quantity = 1,
                    UnitPrice = book.UnitPrice,
                    TotalPrice = book.UnitPrice,
                    BookId = bookId,
                });
            }
            _context.Carts.Update(existingCart);
            await _context.SaveChangesAsync();
            return existingCart;
        }

        public async Task<Cart?> RemoveItemAsync(int cartId, int cartItemId)
        {
            var existingCart = await _context.Carts.Include(cart => cart.Items)
                                           .FirstOrDefaultAsync(c => c.Id == cartId);

            if (existingCart != null)
            {
                var item = existingCart.Items.FirstOrDefault(cart => cart.Id == cartItemId);

                if (item != null)
                {
                    existingCart.Items.Remove(item);
                    _context.Carts.Update(existingCart);
                    _context.CartItems.Remove(item);
                    await _context.SaveChangesAsync();
                }
            }
            return existingCart;
        }

        public async Task<Cart?> ClearCartAsync(string userId)
        {
            var existingCart = await GetByUserIdAsync(userId);

            if (existingCart != null)
            {
                existingCart.Items.Clear();
                _context.Carts.Update(existingCart);
                _context.CartItems.RemoveRange(existingCart.Items);
                await _context.SaveChangesAsync();
            }
            return existingCart;
        }

        private async Task<Cart?> CreateNewCart(string userId)
        {
            var existingUser = await _context.Users.FindAsync(userId);

            if (existingUser != null)
            {
                var newCart = new Cart
                {
                    UserId = userId
                };
                await _context.Carts.AddAsync(newCart);
                await _context.SaveChangesAsync();
                return newCart;
            }
            return null;
        }
    }
}
