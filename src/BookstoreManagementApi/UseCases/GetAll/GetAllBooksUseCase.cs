using BookstoreManagementApi.Communications.Requests;
using BookstoreManagementApi.Communications.Responses;
using BookstoreManagementApi.DataAccess.Repository.Interfaces;
using BookstoreManagementApi.Mapping;

namespace BookstoreManagementApi.UseCases.GetAll;

public class GetAllBooksUseCase
{
    private readonly IBookReadOnlyRepository _repository;
    public GetAllBooksUseCase(IBookReadOnlyRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ResponseGetAllBooks>> Execute(RequestFilterForGetAll requestFilter)
    {
        var books = await _repository.GetAll();

        return books.MapToGetAllResponse(requestFilter);
    }
}
