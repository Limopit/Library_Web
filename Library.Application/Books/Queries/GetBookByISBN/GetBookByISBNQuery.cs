using Library.Application.Books.Queries.GetBookById;
using MediatR;

namespace Library.Application.Books.Queries.GetBookByISBN;

public class GetBookByISBNQuery: IRequest<BookByISBNDto>
{
    public string ISBN { get; set; }
}