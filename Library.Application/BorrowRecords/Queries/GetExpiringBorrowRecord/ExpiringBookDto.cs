namespace Library.Application.BorrowRecords.Queries.GetExpiringBorrowRecord;

public class ExpiringBookDto
{
    public string ISBN { get; set; }
    public string BookName { get; set; }
    public string BookGenre { get; set; }
    public string? BookDescription { get; set; }
    public IList<string> ImageUrls { get; set; }
    public ExpiringRecordDto Record { get; set; }
}