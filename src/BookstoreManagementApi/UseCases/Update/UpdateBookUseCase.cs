using BookstoreManagementApi.Communications.Requests;
using BookstoreManagementApi.DataAccess;
using BookstoreManagementApi.DataAccess.Repository.Interfaces;
using BookstoreManagementApi.Entities;
using BookstoreManagementApi.Exceptions;
using BookstoreManagementApi.Mapping;

namespace BookstoreManagementApi.UseCases.Update;

public class UpdateBookUseCase
{
    private readonly IBookUpdateOnlyRepository _repository;
    private readonly IBookReadOnlyRepository _readOnlyRepository;
    private readonly UnitOfWork _unitOfWork;

    public UpdateBookUseCase(IBookUpdateOnlyRepository repository, UnitOfWork unitOfWork, IBookReadOnlyRepository readOnlyRepository)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _readOnlyRepository = readOnlyRepository;
    }
    public async Task Execute(RequestBook request, Guid Id)
    {
        var book = await _repository.GetById(Id) ?? throw new NotFoundException("Book not found");

        await Validate(request, book);

        request.UpdateBook(book);

        _repository.Update(book);

        await _unitOfWork.Commit();
    }

    private async Task Validate(RequestBook request, Book book)
    {
        if (!book.Title.Equals(request.Title) || !book.Author.Equals(request.Author))
        {
            var bookIsDuplicated = await _readOnlyRepository.BookIsDuplicated(request.Title, request.Author);

            if (bookIsDuplicated)
                throw new InvalidOperationException("The book already exists.");
        }

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
