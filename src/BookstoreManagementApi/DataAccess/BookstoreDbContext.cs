using BookstoreManagementApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookstoreManagementApi.DataAccess;

public class BookstoreDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        var connectionString = "Server=localhost;Database=bookstoredb;Uid=root;Pwd=@Password4321";

        var serverVersion = ServerVersion.AutoDetect(connectionString);

        optionsBuilder.UseMySql(connectionString, serverVersion);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().ToTable("Genres");
    }
}
