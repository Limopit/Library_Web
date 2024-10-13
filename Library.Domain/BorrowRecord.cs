namespace Library.Domain;

public class BorrowRecord
{
    public Guid recordId { get; set; }
    public Guid bookId { get; set; }
    public Book book { get; set; }
    public string userId { get; set; }
    public User user { get; set; }
    public DateTime book_issue_date { get; set; }
    public DateTime book_issue_expiration_date { get; set; }
}