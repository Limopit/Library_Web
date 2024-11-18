namespace Library.Domain;

public class Author
{
    public Guid AuthorId { get; set; }
    public string AuthorFirstname { get; set; }
    public string AuthorLastname { get; set; }
    public DateTime? AuthorBirthday { get; set; }
    public string? AuthorCountry { get; set; }
    public ICollection<Book> Books { get; set; }
}