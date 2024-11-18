namespace Library.Application.Authors.Queries.GetAuthorById;

public class AuthorDetailsDto
{
    public Guid AuthorId { get; set; }
    public string AuthorFirstname { get; set; }
    public string AuthorLastname { get; set; }
    public DateTime? AuthorBirthday { get; set; }
    public string? AuthorCountry { get; set; }
    public ICollection<BookListDto> Books { get; set; } = new List<BookListDto>();
}