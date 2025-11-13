using BookstoreManagementApi.DataAccess;
using BookstoreManagementApi.DataAccess.Repository.Interfaces;
using BookstoreManagementApi.Exceptions;

namespace BookstoreManagementApi.UseCases.Delete;

public class DeleteBookUseCase
{
    private readonly IBookWriteOnlyRepository _repository;
    private readonly IBookReadOnlyRepository _readOnlyRepository;
    private readonly UnitOfWork _unitOfWork;
    public DeleteBookUseCase(IBookWriteOnlyRepository repository, IBookReadOnlyRepository readOnlyRepository, UnitOfWork unitOfWork)
    {
        _repository = repository;
        _readOnlyRepository = readOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(Guid Id)
    {
        var bookExist = await _readOnlyRepository.BookExist(Id);

        if (!bookExist) throw new NotFoundException("Book not found");

        await _repository.Delete(Id);

        await _unitOfWork.Commit();
    }
}
