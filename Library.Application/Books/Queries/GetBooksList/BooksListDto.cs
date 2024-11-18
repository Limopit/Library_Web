namespace Library.Application.Books.Queries.GetBooksList;

public class BooksListDto
{
    public Guid BookId { get; set; }
    public string ISBN { get; set; }
    public string BookName { get; set; }
    public string BookGenre { get; set; }
    public string? BookDescription { get; set; }
    public IList<string> ImageUrls { get; set; }
}