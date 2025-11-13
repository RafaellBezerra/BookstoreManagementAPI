using BookstoreManagementApi.Communications.Responses;
using BookstoreManagementApi.DataAccess.Repository.Interfaces;
using BookstoreManagementApi.Exceptions;
using BookstoreManagementApi.Mapping;

namespace BookstoreManagementApi.UseCases.GetById;

public class GetBookByIdUsecase
{
    private readonly IBookReadOnlyRepository _repository;
    public GetBookByIdUsecase(IBookReadOnlyRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResponseGetBookById> Execute(Guid Id)
    {
        var book = await _repository.GetById(Id) ?? throw new NotFoundException("Book not found");

        return book.MapToGetByIdResponse();
    }
}
