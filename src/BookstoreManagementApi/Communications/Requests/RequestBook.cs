using System;

namespace BookstoreManagementApi.Communications.Requests;

public class RequestBook
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public IList<Enums.Genre> Genre { get; set; } = [];
    public decimal Price { get; set; }
    public int Stock { get; set; }
}
