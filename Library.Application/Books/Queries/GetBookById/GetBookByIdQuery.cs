using MediatR;

namespace Library.Application.Books.Queries.GetBookById;

public class GetBookByIdQuery: IRequest<BookByIdDto>
{
    public Guid BookId { get; set; }
}