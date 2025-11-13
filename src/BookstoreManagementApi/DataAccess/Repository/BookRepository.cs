using BookstoreManagementApi.DataAccess.Repository.Interfaces;
using BookstoreManagementApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookstoreManagementApi.DataAccess.Repository;

public class BookRepository : IBookWriteOnlyRepository, IBookReadOnlyRepository, IBookUpdateOnlyRepository
{
    private readonly BookstoreDbContext _dbContext;
    public BookRepository(BookstoreDbContext dbContext) => _dbContext = dbContext;

    public async Task Add(Book book)
    {
        await _dbContext.Books.AddAsync(book);
    }

    public async Task Delete(Guid Id)
    {
        await _dbContext.Books.Where(book => book.Id == Id).ExecuteDeleteAsync();
    }

    public async Task<List<Book>> GetAll()
    {
        return await _dbContext.Books.Include(book => book.Genres).ToListAsync();
    }

    async Task<Book?> IBookReadOnlyRepository.GetById(Guid Id)
    {
        return await _dbContext.Books.Include(book => book.Genres).AsNoTracking().FirstOrDefaultAsync(book => book.Id == Id);
    }

    async Task<Book?> IBookUpdateOnlyRepository.GetById(Guid Id)
    {
        return await _dbContext.Books.Include(book => book.Genres).FirstOrDefaultAsync(book => book.Id == Id);
    }

    public void Update(Book book)
    {
        _dbContext.Books.Update(book);
    }

    public async Task<bool> BookIsDuplicated(string Title, string Author)
    {
        return await _dbContext.Books.AnyAsync(book => book.Title == Title && book.Author == Author);
    }

    public async Task<bool> BookExist(Guid Id)
    {
        return await _dbContext.Books.AnyAsync(book => book.Id == Id);
    }
}
