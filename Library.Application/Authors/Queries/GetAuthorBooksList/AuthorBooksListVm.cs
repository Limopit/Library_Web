using Library.Domain;

namespace Library.Application.Authors.Queries.GetAuthorBooksList;

public class AuthorBooksListVm
{
    public IList<AuthorBooksListDto> Books { get; set; }
}