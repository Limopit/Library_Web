using MediatR;

namespace Library.Application.Authors.Commands.UpdateAuthor;

public class UpdateAuthorCommand: IRequest
{
    public Guid AuthorId { get; set; }
    public string AuthorFirstname { get; set; }
    public string AuthorLastname { get; set; }
    public DateTime? AuthorBirthday { get; set; }
    public string? AuthorCountry { get; set; }
}