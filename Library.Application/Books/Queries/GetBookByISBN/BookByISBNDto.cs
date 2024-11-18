using Library.Application.Books.Queries.GetBookById;

namespace Library.Application.Books.Queries.GetBookByISBN;

public class BookByISBNDto
{
    public string ISBN { get; set; }
    public string BookName { get; set; }
    public string BookGenre { get; set; }
    public string? BookDescription { get; set; }
    public IList<string> ImageUrls { get; set; }
    public AuthorInfoDto Author { get; set; }
}