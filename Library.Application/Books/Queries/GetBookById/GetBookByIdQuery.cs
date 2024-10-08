using MediatR;

namespace Library.Application.Books.Queries.GetBookById;

public class GetBookByIdQuery: IRequest<BookByIdDto>
{
    public Guid book_id { get; set; }
}