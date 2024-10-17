using MediatR;

namespace Library.Application.Books.Queries.GetBooksList;

public class GetBooksListQuery: IRequest<BooksListVm>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}