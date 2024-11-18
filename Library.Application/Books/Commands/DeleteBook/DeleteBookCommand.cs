using MediatR;

namespace Library.Application.Books.Commands.DeleteBook;

public class DeleteBookCommand: IRequest
{
    public Guid BookId { get; set; }
}