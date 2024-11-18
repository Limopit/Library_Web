namespace Library.Application.Authors.Queries.GetAuthorList;

public class AuthorListDto
{
    public Guid AuthorId { get; set; }
    public string AuthorFirstname { get; set; }
    public string AuthorLastname { get; set; }
}