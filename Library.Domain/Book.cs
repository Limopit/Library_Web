namespace Library.Domain;

public class Book
{
    public Guid BookId { get; set; }
    public string ISBN { get; set; }
    public string BookName { get; set; }
    public string BookGenre { get; set; }
    public string? BookDescription { get; set; }
    public Guid AuthorId { get; set; }
    public Author Author { get; set; }
    public ICollection<BorrowRecord> BorrowRecords { get; set; }
    public string? ImageUrls { get; set; }
}