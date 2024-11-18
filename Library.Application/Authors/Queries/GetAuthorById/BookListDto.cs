namespace Library.Application.Authors.Queries.GetAuthorById;

public class BookListDto
{
    public Guid BookId { get; set; }
    public string BookName { get; set; }
    public string BookGenre { get; set; }
}