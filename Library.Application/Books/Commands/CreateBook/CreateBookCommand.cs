using MediatR;

namespace Library.Application.Books.Commands.CreateBook;

public class CreateBookCommand: IRequest<Guid>
{
    public string ISBN { get; set; }
    public string book_name { get; set; }
    public string book_genre { get; set; }
    public string? book_description { get; set; }
    public Guid author_id { get; set; }
    public DateTime? book_issue_date { get; set; }
    public DateTime? book_issue_expiration_date { get; set; }
}