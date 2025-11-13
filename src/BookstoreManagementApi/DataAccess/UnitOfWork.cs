namespace BookstoreManagementApi.DataAccess;

public class UnitOfWork
{
    private readonly BookstoreDbContext _dbContext;
    public UnitOfWork(BookstoreDbContext dbContext) => _dbContext = dbContext;

    public async Task Commit() => await _dbContext.SaveChangesAsync();
}
