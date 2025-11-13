using BookstoreManagementApi.Entities;

namespace BookstoreManagementApi.DataAccess.Repository.Interfaces;

public interface IBookReadOnlyRepository
{
    Task<List<Book>> GetAll();
    Task<Book?> GetById(Guid Id);
    Task<bool> BookExist(Guid Id);

    Task<bool> BookIsDuplicated(string Title, string Author);
}
