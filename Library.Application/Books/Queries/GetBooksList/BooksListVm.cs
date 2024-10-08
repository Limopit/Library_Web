using Library.Application.Authors.Queries.GetAuthorDetails;

namespace Library.Application.Books.Queries;

public class BooksListVm
{
    public IList<BookListDto> Books { get; set; }
}