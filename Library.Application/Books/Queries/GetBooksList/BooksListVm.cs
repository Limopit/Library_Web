using Library.Application.Authors.Queries.GetAuthorById;

namespace Library.Application.Books.Queries.GetBooksList;

public class BooksListVm
{
    public IList<BooksListDto> Books { get; set; }
}