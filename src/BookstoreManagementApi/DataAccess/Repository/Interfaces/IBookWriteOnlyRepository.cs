using BookstoreManagementApi.Entities;

namespace BookstoreManagementApi.DataAccess.Repository.Interfaces;

public interface IBookWriteOnlyRepository
{
    Task Add(Book book);
    Task Delete(Guid Id);
}
