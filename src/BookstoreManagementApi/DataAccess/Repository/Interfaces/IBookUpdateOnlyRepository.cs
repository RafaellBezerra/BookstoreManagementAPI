using BookstoreManagementApi.Entities;

namespace BookstoreManagementApi.DataAccess.Repository.Interfaces;

public interface IBookUpdateOnlyRepository
{
    Task<Book?> GetById(Guid Id);
    void Update(Book book);
}
