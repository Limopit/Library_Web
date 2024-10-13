namespace Library.Domain;

public class Book
{
    public Guid book_id { get; set; }
    public string ISBN { get; set; }
    public string book_name { get; set; }
    public string book_genre { get; set; }
    public string? book_description { get; set; }
    public Guid author_id { get; set; }
    public Author author { get; set; }
    
    public ICollection<BorrowRecord> borrowRecords { get; set; }
}