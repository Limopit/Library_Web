namespace Library.Application.Books.Queries.GetBookById;

public class BookByIdDto
{
    public string ISBN { get; set; }
    public string BookName { get; set; }
    public string BookGenre { get; set; }
    public string? BookDescription { get; set; }
    public IList<string> ImageUrls { get; set; }
    public AuthorInfoDto Author { get; set; }
}