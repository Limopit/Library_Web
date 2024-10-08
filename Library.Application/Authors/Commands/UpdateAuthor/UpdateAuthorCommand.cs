using Library.Domain;
using MediatR;

namespace Library.Application.Authors.Commands.UpdateAuthor;

public class UpdateAuthorCommand: IRequest
{
    public Guid author_id { get; set; }
    public string author_firstname { get; set; }
    public string author_lastname { get; set; }
    public DateTime? author_birthday { get; set; }
    public string? author_country { get; set; }
    public ICollection<Book> books { get; set; }
}