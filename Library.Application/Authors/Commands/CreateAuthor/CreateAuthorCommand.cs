using Library.Domain;
using MediatR;

namespace Library.Application.Authors.Commands.CreateAuthor;

public class CreateAuthorCommand : IRequest<Guid>
{
    public string AuthorFirstname { get; set; }
    public string AuthorLastname { get; set; }
    public DateTime? AuthorBirthday { get; set; }
    public string? AuthorCountry { get; set; }
    public ICollection<Book> Books { get; set; }
}