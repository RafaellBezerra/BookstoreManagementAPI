using BookstoreManagementApi.Communications.Requests;
using BookstoreManagementApi.Communications.Responses;
using BookstoreManagementApi.DataAccess;
using BookstoreManagementApi.DataAccess.Repository.Interfaces;
using BookstoreManagementApi.Entities;
using BookstoreManagementApi.Exceptions;
using BookstoreManagementApi.Mapping;

namespace BookstoreManagementApi.UseCases.Create;

public class CreateBookUseCase
{
    private readonly IBookWriteOnlyRepository _repository;
    private readonly IBookReadOnlyRepository _readOnlyRepository;
    private readonly UnitOfWork _unitOfWork;
    public CreateBookUseCase(IBookWriteOnlyRepository repository, IBookReadOnlyRepository readOnlyRepository, UnitOfWork unitOfWork)
    {
        _repository = repository;
        _readOnlyRepository = readOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseCreateBook> Execute(RequestBook request)
    {
        Validate(request);

        var bookAlreadyExist = await _readOnlyRepository.BookIsDuplicated(request.Title, request.Author);

        if (bookAlreadyExist)
            throw new InvalidOperationException("The book already exists.");

        Book book = request.ToCreateBook();

        await _repository.Add(book);

        await _unitOfWork.Commit();

        return new ResponseCreateBook()
        {
            Title = request.Title
        };
    }

    private void Validate(RequestBook request)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(request.Title)) errors.Add("Title is required");
        if (string.IsNullOrWhiteSpace(request.Author)) errors.Add("Author is required");
        if (request.Price <= 0) errors.Add("The price cannot be lower than zero.");
        if (request.Stock < 0) errors.Add("The stock cannot be lower than zero.");

        foreach (var genre in request.Genre)
        {
            if (Enum.IsDefined(typeof(Enums.Genre), genre) == false) errors.Add("Invalid genre");
        }

        if (errors.Count > 0)
            throw new ErrorOnValidationException(errors);
    }
}
