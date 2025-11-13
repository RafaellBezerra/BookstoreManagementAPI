namespace BookstoreManagementApi.Communications.Responses;

public class ResponseGetBookById
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public List<Enums.Genre> Genres { get; set; } = [];
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime? UpdateAt { get; set; }
}
