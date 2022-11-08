using GlobalExceptionHandling.Data;
using GlobalExceptionHandling.Entities;
using Microsoft.EntityFrameworkCore;

namespace GlobalExceptionHandling.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetBooksAsync();
        Task<Book?> GetBookAsync(int id);
        Task<Book> AddBookAsync(Book book);
        Task<Book> UpdateBookAsync(Book book);
        Task<bool> DeleteBookAsync(int id);
    }

    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _dbContext;

        public BookService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            var addResult = await _dbContext.Books.AddAsync(book);
            await _dbContext.SaveChangesAsync();
            return addResult.Entity;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
            var deleteResult = _dbContext.Remove(book);
            await _dbContext.SaveChangesAsync();
            return deleteResult != null ? true : false;
        }

        public async Task<Book?> GetBookAsync(int id)
        {
            return await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            return await _dbContext.Books.ToListAsync();
        }

        public async Task<Book> UpdateBookAsync(Book book)
        {
            var updateResult = _dbContext.Books.Update(book);
            await _dbContext.SaveChangesAsync();
            return updateResult.Entity;
        }
    }
}
