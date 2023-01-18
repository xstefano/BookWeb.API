using BookWeb.API.Models;

namespace BookWeb.API.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(int id);
        Task<Book> AddAsync(Book book);
        Task<Book?> UpdateAsync(Book book);
        Task<IEnumerable<Book>> DeleteAsync(int id);
    }
}
