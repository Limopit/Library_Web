using Library.Application.Authors.Queries.GetAuthorById;

namespace Library.Application.Books.Queries.GetBooksList;

public class BooksListVm
{
    public IList<BookListDto> Books { get; set; }
}