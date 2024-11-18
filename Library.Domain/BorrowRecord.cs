namespace Library.Domain;

public class BorrowRecord
{
    public Guid RecordId { get; set; }
    public Guid BookId { get; set; }
    public Book Book { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public DateTime BookIssueDate { get; set; }
    public DateTime BookIssueExpirationDate { get; set; }
}