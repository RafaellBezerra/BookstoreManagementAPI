using BookstoreManagementApi.Communications.Requests;
using BookstoreManagementApi.Communications.Responses;
using BookstoreManagementApi.Entities;

namespace BookstoreManagementApi.Mapping;

public static class BookMappingExtensions
{
    public static void UpdateBook(this RequestBook request, Book book)
    {
        book.Title = request.Title;
        book.Author = request.Author;
        book.Price = request.Price;
        book.Stock = request.Stock;
        book.UpdateAt = DateTime.UtcNow;
        book.Genres = request.Genre.Select(genre => new Entities.Genre { GenreType = genre }).ToList();
    }

    public static Book ToCreateBook(this RequestBook request)
    {
        return new Book
        {
            Title = request.Title,
            Author = request.Author,
            Price = request.Price,
            Stock = request.Stock,
            Genres = request.Genre.Select(g => new Entities.Genre { GenreType = g }).ToList()
        };
    }

    public static List<ResponseGetAllBooks> MapToGetAllResponse(this List<Book> books, RequestFilterForGetAll requestFilter)
    {
        return books.Select(b => new ResponseGetAllBooks
        {
            Id = b.Id,
            Title = b.Title,
            Author = requestFilter.Author ? b.Author : null,
            Price = requestFilter.Price ? b.Price : null,
            Stock = requestFilter.Stock ? b.Stock : null,
            Genres = requestFilter.Genre ? b.Genres.Select(g => g.GenreType).ToList() : null,
            CreateAt = requestFilter.CreateAt ? b.CreateAt : null,
            UpdateAt = requestFilter.UpdateAt ? b.UpdateAt : null,
        }).ToList();
    }

    public static ResponseGetBookById MapToGetByIdResponse(this Book book)
    {
        return new ResponseGetBookById
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Stock = book.Stock,
            CreateAt = book.CreateAt,
            UpdateAt = book.UpdateAt,
            Price = book.Price,
            Genres = book.Genres.Select(b => b.GenreType).ToList(),
        };
    }
}
