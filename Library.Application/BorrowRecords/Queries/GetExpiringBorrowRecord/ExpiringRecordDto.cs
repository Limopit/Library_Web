namespace Library.Application.BorrowRecords.Queries.GetExpiringBorrowRecord;

public class ExpiringRecordDto
{
    public string UserID { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime ExpirationDate { get; set; }
}