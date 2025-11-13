namespace BookstoreManagementApi.Communications.Requests;

public class RequestFilterForGetAll
{
    public bool Author { get; set; } = false;
    public bool Genre { get; set; } = false;
    public bool Price { get; set; } = false;
    public bool Stock { get; set; } = false;
    public bool CreateAt { get; set; } = false;
    public bool UpdateAt { get; set; } = false;
}
