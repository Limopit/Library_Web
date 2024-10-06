using Library.Domain;
using MediatR;

namespace Library.Application.Authors.Commands.CreateAuthor;

public class CreateAuthorCommand : IRequest<Guid>
{
    public string author_firstname { get; set; }
    public string author_lastname { get; set; }
    public DateOnly? author_birthday { get; set; }
    public string? author_country { get; set; }
    public ICollection<Book> books { get; set; }
}