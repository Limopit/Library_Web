using MediatR;

namespace Library.Application.Books.Commands.UpdateBook;

public class UpdateBookCommand: IRequest
{
    public Guid BookId { get; set; }
    public string ISBN { get; set; }
    public string BookName { get; set; }
    public string BookGenre { get; set; }
    public string? BookDescription { get; set; }
    public string? ImageUrls { get; set; }
    public Guid AuthorId { get; set; }
    
}