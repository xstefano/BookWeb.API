using BookWeb.API.Data;
using BookWeb.API.Interfaces;
using BookWeb.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookWeb.API.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationContext _context;
        public BookService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<Book> AddAsync(Book book)
        {
            var newBook = new Book();

            newBook.Title = book.Title;
            newBook.Description = book.Description;
            newBook.Author = book.Author;
            newBook.ImagenURL = book.ImagenURL;
            newBook.UnitPrice = book.UnitPrice;

            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();
            return newBook;
        }

        public async Task<Book?> UpdateAsync(Book book)
        {
            var existingBook = await GetByIdAsync(book.Id);

            if (existingBook == null)
            {
                return null;
            }

            existingBook.Title = book.Title;
            existingBook.Description = book.Description;
            existingBook.Author = book.Author;
            existingBook.ImagenURL = book.ImagenURL;
            existingBook.UnitPrice = book.UnitPrice;

            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<IEnumerable<Book>> DeleteAsync(int id)
        {
            var existingBook = await GetByIdAsync(id);

            if (existingBook == null)
            {
                return null;
            }

            _context.Books.Remove(existingBook);
            await _context.SaveChangesAsync();
            return await GetAllAsync();
        }
    }
}
