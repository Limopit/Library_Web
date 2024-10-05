namespace Library.Domain;

public class Author
{
    public Guid author_id { get; set; }
    public string author_firstname { get; set; }
    public string author_lastname { get; set; }
    public DateOnly? author_birthday { get; set; }
    public string? author_country { get; set; }
    public ICollection<Book> books { get; set; }
}