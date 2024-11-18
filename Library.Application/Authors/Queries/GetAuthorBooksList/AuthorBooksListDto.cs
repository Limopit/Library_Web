namespace Library.Application.Authors.Queries.GetAuthorBooksList;

public class AuthorBooksListDto
{
    public string ISBN { get; set; }
    public string BookName { get; set; }
    public string BookGenre { get; set; }
    public string? BookDescription { get; set; }
}