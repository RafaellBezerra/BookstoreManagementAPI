using BookstoreManagementApi.DataAccess;
using BookstoreManagementApi.DataAccess.Repository;
using BookstoreManagementApi.DataAccess.Repository.Interfaces;
using BookstoreManagementApi.UseCases.Create;
using BookstoreManagementApi.UseCases.Delete;
using BookstoreManagementApi.UseCases.GetAll;
using BookstoreManagementApi.UseCases.GetById;
using BookstoreManagementApi.UseCases.Update;

namespace BookstoreManagementApi;

public static class DependencyInjectionExtension
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddDbContext<BookstoreDbContext>();
        services.AddScoped<UnitOfWork>();

        AddRepository(services);
        AddUseCases(services);
    }

    private static void AddRepository(IServiceCollection services)
    {
        services.AddScoped<IBookReadOnlyRepository, BookRepository>();
        services.AddScoped<IBookWriteOnlyRepository, BookRepository>();
        services.AddScoped<IBookUpdateOnlyRepository, BookRepository>();
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<CreateBookUseCase>();
        services.AddScoped<GetAllBooksUseCase>();
        services.AddScoped<GetBookByIdUsecase>();
        services.AddScoped<DeleteBookUseCase>();
        services.AddScoped<UpdateBookUseCase>();
    }
}
