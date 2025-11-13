using System;

namespace BookstoreManagementApi.Entities;

public class Genre
{
    public long Id { get; set; }
    public Enums.Genre GenreType { get; set; }

    public Guid BookId { get; set; }
    public Book Book { get; set; } = default!;
}
